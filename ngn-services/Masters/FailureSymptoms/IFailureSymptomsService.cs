using ngn_services.Masters;

namespace ngn_services.Masters.FailureSymptoms;

public interface IFailureSymptomsService
{
    public Task<IReadOnlyList<FailureSymptomsResponse>> GetFailureSymptomsAsync(int? projectId, CancellationToken ct);
    public Task<FailureSymptomsResponse?> GetFailureSymptomAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<FailureSymptomsResponse>> CreateFailureSymptomAsync(FailureSymptomsRequest r, CancellationToken ct);
    public Task<MasterOperationResult<FailureSymptomsResponse>> UpdateFailureSymptomAsync(int id, FailureSymptomsRequest r, CancellationToken ct);
}
