using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;

namespace ngn_services.ProjectGroups;

public sealed class ProjectGroupService(AppDbContext dbContext) : IProjectGroupService
{
    public async Task<IReadOnlyList<ProjectGroupResponse>> GetAllAsync(bool includeInactive, CancellationToken cancellationToken)
    {
        var query = dbContext.ProjectGroups.AsNoTracking();
        if (!includeInactive) query = query.Where(x => x.IsActive);

        return await query.OrderBy(x => x.ProjectGroupName)
            .Select(x => new ProjectGroupResponse(x.ProjectGroupId, x.ProjectGroupName,
                x.ProjectGroupDescription, x.IsActive, x.CreatedDate, x.UpdatedDate))
            .ToListAsync(cancellationToken);
    }

    public Task<ProjectGroupResponse?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        dbContext.ProjectGroups.AsNoTracking().Where(x => x.ProjectGroupId == id)
            .Select(x => new ProjectGroupResponse(x.ProjectGroupId, x.ProjectGroupName,
                x.ProjectGroupDescription, x.IsActive, x.CreatedDate, x.UpdatedDate))
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<ProjectGroupOperationResult> CreateAsync(ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var name = request.ProjectGroupName.Trim();
        if (await dbContext.ProjectGroups.AnyAsync(x => x.ProjectGroupName == name, cancellationToken))
            return new ProjectGroupOperationResult(null, Conflict: true);

        var now = DateTime.UtcNow;
        var entity = new ProjectGroupMaster {
            ProjectGroupName = name,
            ProjectGroupDescription = request.ProjectGroupDescription?.Trim(),
            IsActive = true, CreatedDate = now, UpdatedDate = now
        };
        dbContext.ProjectGroups.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ProjectGroupOperationResult(ToResponse(entity));
    }

    public async Task<ProjectGroupOperationResult> UpdateAsync(int id, ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups.SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);
        if (entity is null) return new ProjectGroupOperationResult(null, NotFound: true);

        var name = request.ProjectGroupName.Trim();
        if (await dbContext.ProjectGroups.AnyAsync(x => x.ProjectGroupId != id && x.ProjectGroupName == name, cancellationToken))
            return new ProjectGroupOperationResult(null, Conflict: true);

        entity.ProjectGroupName = name;
        entity.ProjectGroupDescription = request.ProjectGroupDescription?.Trim();
        entity.UpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ProjectGroupOperationResult(ToResponse(entity));
    }

    public async Task<ProjectGroupOperationResult> SetStatusAsync(int id, ProjectGroupStatusRequest request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups.SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);
        if (entity is null) return new ProjectGroupOperationResult(null, NotFound: true);

        entity.IsActive = request.IsActive;
        entity.UpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ProjectGroupOperationResult(ToResponse(entity));
    }

    public async Task<ProjectGroupOperationResult> SoftDeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups.SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);
        if (entity is null) return new ProjectGroupOperationResult(null, NotFound: true);

        if (entity.IsActive) {
            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return new ProjectGroupOperationResult(ToResponse(entity));
    }

    private static ProjectGroupResponse ToResponse(ProjectGroupMaster entity) =>
        new(entity.ProjectGroupId, entity.ProjectGroupName, entity.ProjectGroupDescription,
            entity.IsActive, entity.CreatedDate, entity.UpdatedDate);
}