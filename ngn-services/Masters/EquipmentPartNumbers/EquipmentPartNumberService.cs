using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.EquipmentPartNumbers;

public sealed class EquipmentPartNumberService(AppDbContext db) : IEquipmentPartNumberService
{
    public async Task<IReadOnlyList<EquipmentPartNumberResponse>> GetPartNumbersAsync(bool includeInactive, int? equipmentId, CancellationToken ct)
        {
            var q = db.EquipmentPartNumbers.AsNoTracking();
            if (!includeInactive) q = q.Where(x => x.IsActive);
            if (equipmentId.HasValue) q = q.Where(x => x.EquipmentId == equipmentId.Value);
            var rows = await q.Include(x => x.Equipment).OrderBy(x => x.PartNumber).ToListAsync(ct);
            return rows.Select(PartDto).ToList();
        }

    public async Task<EquipmentPartNumberResponse?> GetPartNumberAsync(int id, CancellationToken ct)
        {
            var x = await db.EquipmentPartNumbers.AsNoTracking().Include(x => x.Equipment).SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
            return x is null ? null : PartDto(x);
        }

    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> CreatePartNumberAsync(EquipmentPartNumberRequest r, CancellationToken ct)
        {
            if (!await db.Equipment.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.IsActive, ct)) return new(null, Error: "The equipment does not exist or is inactive.");
            var part = r.PartNumber.Trim();
            if (await db.EquipmentPartNumbers.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.PartNumber == part, ct)) return new(null, Conflict: true);
            var now = DateTime.UtcNow;
            var e = new EquipmentPartNumber { EquipmentId = r.EquipmentId, PartNumber = part, IsActive = true, CreatedDate = now, UpdatedDate = now };
            db.EquipmentPartNumbers.Add(e); await db.SaveChangesAsync(ct); return new(await GetPartNumberAsync(e.EquipmentPartNumberId, ct));
        }

    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> UpdatePartNumberAsync(int id, EquipmentPartNumberRequest r, CancellationToken ct)
        {
            var e = await db.EquipmentPartNumbers.SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (!await db.Equipment.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.IsActive, ct)) return new(null, Error: "The equipment does not exist or is inactive.");
            var part = r.PartNumber.Trim();
            if (await db.EquipmentPartNumbers.AnyAsync(x => x.EquipmentPartNumberId != id && x.EquipmentId == r.EquipmentId && x.PartNumber == part, ct)) return new(null, Conflict: true);
            e.EquipmentId = r.EquipmentId; e.PartNumber = part; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
            return new(await GetPartNumberAsync(id, ct));
        }

    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> SetPartNumberStatusAsync(int id, bool isActive, CancellationToken ct)
        {
            var e = await db.EquipmentPartNumbers.SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
            if (e is null) return new(null, NotFound: true);
            if (isActive && !await db.Equipment.AnyAsync(x => x.EquipmentId == e.EquipmentId && x.IsActive, ct)) return new(null, Error: "Cannot activate a part number while its equipment is inactive.");
            e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct); return new(await GetPartNumberAsync(id, ct));
        }

    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> DeletePartNumberAsync(int id, CancellationToken ct) => await SetPartNumberStatusAsync(id, false, ct);

    private static EquipmentPartNumberResponse PartDto(EquipmentPartNumber x) => new(x.EquipmentPartNumberId, x.EquipmentId, x.Equipment.EquipmentName, x.PartNumber, x.IsActive, x.CreatedDate, x.UpdatedDate);
}
