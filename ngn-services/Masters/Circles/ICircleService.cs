using ngn_services.Masters;

namespace ngn_services.Masters.Circles;

public interface ICircleService
{
    public Task<IReadOnlyList<CircleResponse>> GetCirclesAsync(CancellationToken ct);
    public Task<CircleResponse?> GetCircleAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<CircleResponse>> CreateCircleAsync(CircleRequest r, CancellationToken ct);
    public Task<MasterOperationResult<CircleResponse>> UpdateCircleAsync(int id, CircleRequest r, CancellationToken ct);
}
