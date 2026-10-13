using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.FailureSymptoms;

public sealed class FailureSymptomsService(AppDbContext db) : IFailureSymptomsService
{
    public async Task<IReadOnlyList<FailureSymptomsResponse>> GetFailureSymptomsAsync(int? projectId, CancellationToken ct)
        {
            var q = db.FailureSymptoms.AsNoTracking(); if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
            var rows = await q.Include(x => x.Project).OrderBy(x => x.FailureSymptomsName).ToListAsync(ct);
            return rows.Select(FailureDto).ToList();
        }

    public async Task<FailureSymptomsResponse?> GetFailureSymptomAsync(int id, CancellationToken ct)
        {
            var x = await db.FailureSymptoms.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.FailureSymptomsId == id, ct);
            return x is null ? null : FailureDto(x);
        }

    public async Task<MasterOperationResult<FailureSymptomsResponse>> CreateFailureSymptomAsync(FailureSymptomsRequest r, CancellationToken ct)
        {
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            var name = r.FailureSymptomsName.Trim();
            if (await db.FailureSymptoms.AnyAsync(x => x.ProjectId == r.ProjectId && x.FailureSymptomsName == name, ct)) return new(null, Conflict: true);
            var e = new FailureSymptomsMaster { ProjectId = r.ProjectId, FailureSymptomsName = name };
            db.FailureSymptoms.Add(e); await db.SaveChangesAsync(ct); return new(await GetFailureSymptomAsync(e.FailureSymptomsId, ct));
        }

    public async Task<MasterOperationResult<FailureSymptomsResponse>> UpdateFailureSymptomAsync(int id, FailureSymptomsRequest r, CancellationToken ct)
        {
            var e = await db.FailureSymptoms.SingleOrDefaultAsync(x => x.FailureSymptomsId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            var name = r.FailureSymptomsName.Trim();
            if (await db.FailureSymptoms.AnyAsync(x => x.FailureSymptomsId != id && x.ProjectId == r.ProjectId && x.FailureSymptomsName == name, ct)) return new(null, Conflict: true);
            e.ProjectId = r.ProjectId; e.FailureSymptomsName = name; await db.SaveChangesAsync(ct); return new(await GetFailureSymptomAsync(id, ct));
        }

    private static FailureSymptomsResponse FailureDto(FailureSymptomsMaster x) => new(x.FailureSymptomsId, x.ProjectId, x.Project.ProjectName, x.FailureSymptomsName);
}
