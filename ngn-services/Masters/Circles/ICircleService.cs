using ngn_services.Masters;

namespace ngn_services.Masters.Circles;

public interface ICircleService
{
    public async Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct);
    public Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<CircleByZoneResponse>> CreateCircleByZoneAsync(CircleByZoneRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<CircleByZoneResponse>> UpdateCircleByZoneAsync(int id, CircleByZoneRequest r, CancellationToken ct);
    public async Task<IReadOnlyList<CircleResponse>> GetCirclesAsync(CancellationToken ct);
    public Task<CircleResponse?> GetCircleAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<CircleResponse>> CreateCircleAsync(CircleRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<CircleResponse>> UpdateCircleAsync(int id, CircleRequest r, CancellationToken ct);
}
