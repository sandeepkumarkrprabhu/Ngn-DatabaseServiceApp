using ngn_services.Masters;

namespace ngn_services.Masters.Statuses;

public interface IStatusService
{
    public Task<IReadOnlyList<StatusResponse>> GetStatusesAsync(bool includeInactive, CancellationToken ct);
    public Task<StatusResponse?> GetStatusAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<StatusResponse>> CreateStatusAsync(StatusRequest r, CancellationToken ct);
    public Task<MasterOperationResult<StatusResponse>> UpdateStatusAsync(int id, StatusRequest r, CancellationToken ct);
    public Task<MasterOperationResult<StatusResponse>> SetStatusActiveAsync(int id, bool isActive, CancellationToken ct);
    public Task<MasterOperationResult<StatusResponse>> DeleteStatusAsync(int id, CancellationToken ct);
}
