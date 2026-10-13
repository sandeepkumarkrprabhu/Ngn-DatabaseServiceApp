using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.CallTypes;

public sealed class CallTypeService(AppDbContext db) : ICallTypeService
{
    public async Task<IReadOnlyList<CallTypeResponse>> GetCallTypesAsync(CancellationToken ct)
    {
        var rows = await db.CallTypeMaster.AsNoTracking().OrderBy(x => x.CallName).ToListAsync(ct);
        return rows.Select(CallDto).ToList();
    }

    public async Task<CallTypeResponse?> GetCallTypeAsync(int id, CancellationToken ct)
    {
        var x = await db.CallTypeMaster.AsNoTracking().SingleOrDefaultAsync(x => x.CallTypeId == id, ct);
        return x is null ? null : CallDto(x);
    }

    public async Task<MasterOperationResult<CallTypeResponse>> CreateCallTypeAsync(CallTypeRequest r, CancellationToken ct)
        {
            var name = r.CallName.Trim();
            if (await db.CallTypeMaster.AnyAsync(x => x.CallName == name, ct)) return new(null, Conflict: true);
            var e = new CallTypeMaster { CallName = name }; db.CallTypeMaster.Add(e); await db.SaveChangesAsync(ct); return new(CallDto(e));
        }

    public async Task<MasterOperationResult<CallTypeResponse>> UpdateCallTypeAsync(int id, CallTypeRequest r, CancellationToken ct)
        {
            var e = await db.CallTypeMaster.SingleOrDefaultAsync(x => x.CallTypeId == id, ct); if (e is null) return new(null, NotFound: true);
            var name = r.CallName.Trim();
            if (await db.CallTypeMaster.AnyAsync(x => x.CallTypeId != id && x.CallName == name, ct)) return new(null, Conflict: true);
            e.CallName = name; await db.SaveChangesAsync(ct); return new(CallDto(e));
        }

    private static CallTypeResponse CallDto(CallTypeMaster x) => new(x.CallTypeId, x.CallName);
}
