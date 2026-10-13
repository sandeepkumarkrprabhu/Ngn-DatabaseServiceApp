using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.Equipment;

public sealed class EquipmentService(AppDbContext db) : IEquipmentService
{
    public async Task<IReadOnlyList<EquipmentResponse>> GetEquipmentAsync(bool includeInactive, int? projectId, CancellationToken ct)
        {
            var q = db.Equipment.AsNoTracking();
            if (!includeInactive) q = q.Where(x => x.IsActive);
            if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
            var rows = await q.Include(x => x.Project).OrderBy(x => x.EquipmentName).ToListAsync(ct);
            return rows.Select(EquipmentDto).ToList();
        }

    public async Task<EquipmentResponse?> GetEquipmentByIdAsync(int id, CancellationToken ct)
        {
            var x = await db.Equipment.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
            return x is null ? null : EquipmentDto(x);
        }

    public async Task<MasterOperationResult<EquipmentResponse>> CreateEquipmentAsync(EquipmentRequest r, CancellationToken ct)
        {
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            var name = r.EquipmentName.Trim();
            if (await db.Equipment.AnyAsync(x => x.ProjectId == r.ProjectId && x.EquipmentName == name, ct)) return new(null, Conflict: true);
            var now = DateTime.UtcNow;
            var e = new EquipmentMaster { EquipmentName = name, EquipmentDescription = r.EquipmentDescription?.Trim(), ProjectId = r.ProjectId, IsActive = true, CreatedDate = now, UpdatedDate = now };
            db.Equipment.Add(e); await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(e.EquipmentId, ct));
        }

    public async Task<MasterOperationResult<EquipmentResponse>> UpdateEquipmentAsync(int id, EquipmentRequest r, CancellationToken ct)
        {
            var e = await db.Equipment.SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
            var name = r.EquipmentName.Trim();
            if (await db.Equipment.AnyAsync(x => x.EquipmentId != id && x.ProjectId == r.ProjectId && x.EquipmentName == name, ct)) return new(null, Conflict: true);
            e.EquipmentName = name; e.EquipmentDescription = r.EquipmentDescription?.Trim(); e.ProjectId = r.ProjectId; e.UpdatedDate = DateTime.UtcNow;
            await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(id, ct));
        }

    public async Task<MasterOperationResult<EquipmentResponse>> SetEquipmentStatusAsync(int id, bool isActive, CancellationToken ct)
        {
            var e = await db.Equipment.SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (isActive && !await db.ProjectMasters.AnyAsync(x => x.ProjectId == e.ProjectId && x.IsActive, ct)) return new(null, Error: "Cannot activate equipment while its project is inactive.");
            e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(id, ct));
        }

    public async Task<MasterOperationResult<EquipmentResponse>> DeleteEquipmentAsync(int id, CancellationToken ct) => await SetEquipmentStatusAsync(id, false, ct);

    private static EquipmentResponse EquipmentDto(EquipmentMaster x) => new(x.EquipmentId, x.EquipmentName, x.EquipmentDescription, x.ProjectId, x.Project.ProjectName, x.IsActive, x.CreatedDate, x.UpdatedDate);
}
