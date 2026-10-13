using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;
using ngn_services.Masters;

namespace ngn_services.Masters.Circles;

public sealed class CircleService(AppDbContext db) : ICircleService
{
    public async Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct) =>
            await db.CircleByZones.AsNoTracking().OrderBy(x => x.Zone).ThenBy(x => x.CircleName).Select(x => ZoneDto(x)).ToListAsync(ct);

    public Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct) =>
            db.CircleByZones.AsNoTracking().Where(x => x.CircleZoneId == id).Select(x => ZoneDto(x)).SingleOrDefaultAsync(ct);

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

    public async Task<IReadOnlyList<CircleResponse>> GetCirclesAsync(CancellationToken ct) =>
            await db.CircleMaster.AsNoTracking().OrderBy(x => x.CircleName).ThenBy(x => x.CityName).Select(x => CircleDto(x)).ToListAsync(ct);

    public Task<CircleResponse?> GetCircleAsync(int id, CancellationToken ct) =>
            db.CircleMaster.AsNoTracking().Where(x => x.CircleCityId == id).Select(x => CircleDto(x)).SingleOrDefaultAsync(ct);

    public async Task<MasterOperationResult<CircleResponse>> CreateCircleAsync(CircleRequest r, CancellationToken ct)
        {
            var e = new CircleMaster { CircleCode = r.CircleCode.Trim(), CircleName = r.CircleName.Trim(), CityCode = r.CityCode.Trim(), CityName = r.CityName.Trim() };
            db.CircleMaster.Add(e); await db.SaveChangesAsync(ct); return new(CircleDto(e));
        }

    public async Task<MasterOperationResult<CircleResponse>> UpdateCircleAsync(int id, CircleRequest r, CancellationToken ct)
        {
            var e = await db.CircleMaster.SingleOrDefaultAsync(x => x.CircleCityId == id, ct); if (e is null) return new(null, NotFound: true);
            e.CircleCode = r.CircleCode.Trim(); e.CircleName = r.CircleName.Trim(); e.CityCode = r.CityCode.Trim(); e.CityName = r.CityName.Trim();
            await db.SaveChangesAsync(ct); return new(CircleDto(e));
        }

    private static CircleResponse CircleDto(CircleMaster x) => new(x.CircleCityId, x.CircleCode, x.CircleName, x.CityCode, x.CityName);
}
