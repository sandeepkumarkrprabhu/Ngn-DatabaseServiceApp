using ngn_services.Masters;

namespace ngn_services.Masters.CallTypes;

public interface ICallTypeService
{
    public Task<IReadOnlyList<CallTypeResponse>> GetCallTypesAsync(CancellationToken ct);
    public Task<CallTypeResponse?> GetCallTypeAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<CallTypeResponse>> CreateCallTypeAsync(CallTypeRequest r, CancellationToken ct);
    public Task<MasterOperationResult<CallTypeResponse>> UpdateCallTypeAsync(int id, CallTypeRequest r, CancellationToken ct);
}
