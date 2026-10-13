using ngn_services.Masters;

namespace ngn_services.Masters.CallTypes;

public interface ICallTypeService
{
    public async Task<IReadOnlyList<CallTypeResponse>> GetCallTypesAsync(CancellationToken ct);
    public Task<CallTypeResponse?> GetCallTypeAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<CallTypeResponse>> CreateCallTypeAsync(CallTypeRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<CallTypeResponse>> UpdateCallTypeAsync(int id, CallTypeRequest r, CancellationToken ct);
}
