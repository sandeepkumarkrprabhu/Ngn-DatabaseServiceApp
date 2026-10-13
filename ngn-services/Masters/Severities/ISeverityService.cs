using ngn_services.Masters;

namespace ngn_services.Masters.Severities;

public interface ISeverityService
{
    public Task<IReadOnlyList<SeverityResponse>> GetSeveritiesAsync(int? projectId, CancellationToken ct);
    public Task<SeverityResponse?> GetSeverityAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<SeverityResponse>> CreateSeverityAsync(SeverityRequest r, CancellationToken ct);
    public Task<MasterOperationResult<SeverityResponse>> UpdateSeverityAsync(int id, SeverityRequest r, CancellationToken ct);
}
