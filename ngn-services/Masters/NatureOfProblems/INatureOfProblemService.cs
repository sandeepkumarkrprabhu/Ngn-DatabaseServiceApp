using ngn_services.Masters;

namespace ngn_services.Masters.NatureOfProblems;

public interface INatureOfProblemService
{
    public Task<IReadOnlyList<NatureOfProblemResponse>> GetNaturesAsync(int? projectItemId, CancellationToken ct);
    public Task<NatureOfProblemResponse?> GetNatureAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<NatureOfProblemResponse>> CreateNatureAsync(NatureOfProblemRequest r, CancellationToken ct);
    public Task<MasterOperationResult<NatureOfProblemResponse>> UpdateNatureAsync(int id, NatureOfProblemRequest r, CancellationToken ct);
}
