using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.NatureOfProblems;

public sealed class NatureOfProblemService(AppDbContext db) : INatureOfProblemService
{
    public async Task<IReadOnlyList<NatureOfProblemResponse>> GetNaturesAsync(int? projectItemId, CancellationToken ct)
        {
            var q = db.NatureOfProblem.AsNoTracking(); if (projectItemId.HasValue) q = q.Where(x => x.ProjectItemId == projectItemId.Value);
            var rows = await q.Include(x => x.ProjectItem).OrderBy(x => x.NatureOfProblemName).ToListAsync(ct);
            return rows.Select(NatureDto).ToList();
        }

    public async Task<NatureOfProblemResponse?> GetNatureAsync(int id, CancellationToken ct)
        {
            var x = await db.NatureOfProblem.AsNoTracking().Include(x => x.ProjectItem).SingleOrDefaultAsync(x => x.NatureOfProblemId == id, ct);
            return x is null ? null : NatureDto(x);
        }

    public async Task<MasterOperationResult<NatureOfProblemResponse>> CreateNatureAsync(NatureOfProblemRequest r, CancellationToken ct)
        {
            if (r.ProjectItemId.HasValue && !await db.ProjectItemDetails.AnyAsync(x => x.ProjectItemId == r.ProjectItemId.Value, ct)) return new(null, Error: "The project item was not found.");
            var name = r.NatureOfProblemName.Trim();
            if (await db.NatureOfProblem.AnyAsync(x => x.ProjectItemId == r.ProjectItemId && x.NatureOfProblemName == name, ct)) return new(null, Conflict: true);
            var e = new NatureOfProblemMaster { NatureOfProblemName = name, ProjectItemId = r.ProjectItemId };
            db.NatureOfProblem.Add(e); await db.SaveChangesAsync(ct); return new(await GetNatureAsync(e.NatureOfProblemId, ct));
        }

    public async Task<MasterOperationResult<NatureOfProblemResponse>> UpdateNatureAsync(int id, NatureOfProblemRequest r, CancellationToken ct)
        {
            var e = await db.NatureOfProblem.SingleOrDefaultAsync(x => x.NatureOfProblemId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (r.ProjectItemId.HasValue && !await db.ProjectItemDetails.AnyAsync(x => x.ProjectItemId == r.ProjectItemId.Value, ct)) return new(null, Error: "The project item was not found.");
            var name = r.NatureOfProblemName.Trim();
            if (await db.NatureOfProblem.AnyAsync(x => x.NatureOfProblemId != id && x.ProjectItemId == r.ProjectItemId && x.NatureOfProblemName == name, ct)) return new(null, Conflict: true);
            e.NatureOfProblemName = name; e.ProjectItemId = r.ProjectItemId; await db.SaveChangesAsync(ct); return new(await GetNatureAsync(id, ct));
        }

    private static NatureOfProblemResponse NatureDto(NatureOfProblemMaster x) => new(x.NatureOfProblemId, x.NatureOfProblemName, x.ProjectItemId, x.ProjectItem == null ? null : x.ProjectItem.ItemName);
}
