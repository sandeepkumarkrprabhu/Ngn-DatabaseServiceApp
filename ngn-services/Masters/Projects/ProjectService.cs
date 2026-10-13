using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.Projects;

public sealed class ProjectService(AppDbContext db) : IProjectService
{
    public async Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(bool includeInactive, CancellationToken ct)
        {
            var q = db.ProjectMasters.AsNoTracking();
            if (!includeInactive) q = q.Where(x => x.IsActive);
            var rows = await q.Include(x => x.ProjectGroup).OrderBy(x => x.ProjectName).ToListAsync(ct);
            return rows.Select(ProjectDto).ToList();
        }

    public async Task<ProjectResponse?> GetProjectAsync(int id, CancellationToken ct)
        {
            var x = await db.ProjectMasters.AsNoTracking().Include(x => x.ProjectGroup).SingleOrDefaultAsync(x => x.ProjectId == id, ct);
            return x is null ? null : ProjectDto(x);
        }

    public async Task<MasterOperationResult<ProjectResponse>> CreateProjectAsync(ProjectRequest r, CancellationToken ct)
        {
            var name = r.ProjectName.Trim();
            if (!await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == r.ProjectGroupId && x.IsActive, ct))
                return new(null, Error: "The project group does not exist or is inactive.");
            if (await db.ProjectMasters.AnyAsync(x => x.ProjectName == name, ct)) return new(null, Conflict: true);
            var now = DateTime.UtcNow;
            var e = new ProjectMaster { ProjectName = name, ProjectGroupId = r.ProjectGroupId, IsActive = true, CreatedDate = now, UpdatedDate = now };
            db.ProjectMasters.Add(e); await db.SaveChangesAsync(ct);
            return new(await GetProjectAsync(e.ProjectId, ct));
        }

    public async Task<MasterOperationResult<ProjectResponse>> UpdateProjectAsync(int id, ProjectRequest r, CancellationToken ct)
        {
            var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (!await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == r.ProjectGroupId && x.IsActive, ct))
                return new(null, Error: "The project group does not exist or is inactive.");
            var name = r.ProjectName.Trim();
            if (await db.ProjectMasters.AnyAsync(x => x.ProjectId != id && x.ProjectName == name, ct)) return new(null, Conflict: true);
            e.ProjectName = name; e.ProjectGroupId = r.ProjectGroupId; e.UpdatedDate = DateTime.UtcNow;
            await db.SaveChangesAsync(ct); return new(await GetProjectAsync(id, ct));
        }

    public async Task<MasterOperationResult<ProjectResponse>> SetProjectStatusAsync(int id, bool isActive, CancellationToken ct)
        {
            var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (isActive && !await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == e.ProjectGroupId && x.IsActive, ct))
                return new(null, Error: "Cannot activate a project while its project group is inactive.");
            e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
            return new(await GetProjectAsync(id, ct));
        }

    public async Task<MasterOperationResult<ProjectResponse>> DeleteProjectAsync(int id, CancellationToken ct)
        {
            var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
            if (e is null) return new(null, NotFound: true);
            e.IsActive = false; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
            return new(await GetProjectAsync(id, ct));
        }

    private static ProjectResponse ProjectDto(ProjectMaster x) => new(x.ProjectId, x.ProjectName, x.ProjectGroupId, x.ProjectGroup.ProjectGroupName, x.IsActive, x.CreatedDate, x.UpdatedDate);
}
