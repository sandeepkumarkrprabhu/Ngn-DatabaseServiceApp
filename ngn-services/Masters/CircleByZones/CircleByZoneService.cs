using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.CircleByZones;

public sealed class CircleByZoneService(AppDbContext db) : ICircleByZoneService
{
    public async Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct)
    {
        var rows = await db.CircleByZones.AsNoTracking().OrderBy(x => x.Zone).ThenBy(x => x.CircleName).ToListAsync(ct);
        return rows.Select(ZoneDto).ToList();
    }

    public async Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct)
    {
        var x = await db.CircleByZones.AsNoTracking().SingleOrDefaultAsync(x => x.CircleZoneId == id, ct);
        return x is null ? null : ZoneDto(x);
    }

    public async Task<MasterOperationResult<CircleByZoneResponse>> CreateCircleByZoneAsync(CircleByZoneRequest r, CancellationToken ct)
        {
            var zone = r.Zone.Trim(); var circle = r.CircleName.Trim();
            if (await db.CircleByZones.AnyAsync(x => x.Zone == zone && x.CircleName == circle, ct)) return new(null, Conflict: true);
            var e = new CircleByZoneMaster { Zone = zone, CircleName = circle }; db.CircleByZones.Add(e); await db.SaveChangesAsync(ct); return new(ZoneDto(e));
        }

    public async Task<MasterOperationResult<CircleByZoneResponse>> UpdateCircleByZoneAsync(int id, CircleByZoneRequest r, CancellationToken ct)
        {
            var e = await db.CircleByZones.SingleOrDefaultAsync(x => x.CircleZoneId == id, ct); if (e is null) return new(null, NotFound: true);
            var zone = r.Zone.Trim(); var circle = r.CircleName.Trim();
            if (await db.CircleByZones.AnyAsync(x => x.CircleZoneId != id && x.Zone == zone && x.CircleName == circle, ct)) return new(null, Conflict: true);
            e.Zone = zone; e.CircleName = circle; await db.SaveChangesAsync(ct); return new(ZoneDto(e));
        }

    private static CircleByZoneResponse ZoneDto(CircleByZoneMaster x) => new(x.CircleZoneId, x.Zone, x.CircleName);
}
