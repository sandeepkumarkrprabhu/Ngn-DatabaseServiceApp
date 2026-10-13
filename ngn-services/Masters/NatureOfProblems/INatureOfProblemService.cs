using ngn_services.Masters;

namespace ngn_services.Masters.NatureOfProblems;

public interface INatureOfProblemService
{
    public async Task<IReadOnlyList<NatureOfProblemResponse>> GetNaturesAsync(int? projectItemId, CancellationToken ct);
    public async Task<NatureOfProblemResponse?> GetNatureAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<NatureOfProblemResponse>> CreateNatureAsync(NatureOfProblemRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<NatureOfProblemResponse>> UpdateNatureAsync(int id, NatureOfProblemRequest r, CancellationToken ct);
}
