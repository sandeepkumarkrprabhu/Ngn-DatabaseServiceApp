using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.Severities;

public sealed class SeverityService(AppDbContext db) : ISeverityService
{
    public async Task<IReadOnlyList<SeverityResponse>> GetSeveritiesAsync(int? projectId, CancellationToken ct)
        {
            var q = db.SeverityMasters.AsNoTracking();
            if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
            var rows = await q.Include(x => x.Project).OrderBy(x => x.ProjectId).ThenBy(x => x.SeverityName).ToListAsync(ct);
            return rows.Select(SeverityDto).ToList();
        }

    public async Task<SeverityResponse?> GetSeverityAsync(int id, CancellationToken ct)
        {
            var x = await db.SeverityMasters.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.SeverityId == id, ct);
            return x is null ? null : SeverityDto(x);
        }

    public async Task<MasterOperationResult<SeverityResponse>> CreateSeverityAsync(SeverityRequest r, CancellationToken ct)
        {
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            var e = new SeverityMaster { ProjectId = r.ProjectId, SeverityCode = r.SeverityCode.Trim(), SeverityName = r.SeverityName.Trim(), Level = r.Level?.Trim(), ResponseSLA = r.ResponseSLA, ResolutionSLA = r.ResolutionSLA, Description = r.Description?.Trim() };
            db.SeverityMasters.Add(e); await db.SaveChangesAsync(ct); return new(await GetSeverityAsync(e.SeverityId, ct));
        }

    public async Task<MasterOperationResult<SeverityResponse>> UpdateSeverityAsync(int id, SeverityRequest r, CancellationToken ct)
        {
            var e = await db.SeverityMasters.SingleOrDefaultAsync(x => x.SeverityId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            e.ProjectId = r.ProjectId; e.SeverityCode = r.SeverityCode.Trim(); e.SeverityName = r.SeverityName.Trim(); e.Level = r.Level?.Trim(); e.ResponseSLA = r.ResponseSLA; e.ResolutionSLA = r.ResolutionSLA; e.Description = r.Description?.Trim();
            await db.SaveChangesAsync(ct); return new(await GetSeverityAsync(id, ct));
        }

    private static SeverityResponse SeverityDto(SeverityMaster x) => new(x.SeverityId, x.ProjectId, x.Project.ProjectName, x.SeverityCode, x.SeverityName, x.Level, x.ResponseSLA, x.ResolutionSLA, x.Description);
}
