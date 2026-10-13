using ngn_services.Masters;

namespace ngn_services.Masters.Severities;

public interface ISeverityService
{
    public async Task<IReadOnlyList<SeverityResponse>> GetSeveritiesAsync(int? projectId, CancellationToken ct);
    public async Task<SeverityResponse?> GetSeverityAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<SeverityResponse>> CreateSeverityAsync(SeverityRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<SeverityResponse>> UpdateSeverityAsync(int id, SeverityRequest r, CancellationToken ct);
}
