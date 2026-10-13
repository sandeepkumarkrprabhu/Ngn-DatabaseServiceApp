using ngn_services.Masters;

namespace ngn_services.Masters.CircleByZones;

public interface ICircleByZoneService
{
    public Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct);
    public Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<CircleByZoneResponse>> CreateCircleByZoneAsync(CircleByZoneRequest r, CancellationToken ct);
    public Task<MasterOperationResult<CircleByZoneResponse>> UpdateCircleByZoneAsync(int id, CircleByZoneRequest r, CancellationToken ct);
}
