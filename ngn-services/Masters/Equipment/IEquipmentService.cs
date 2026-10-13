using ngn_services.Masters;

namespace ngn_services.Masters.Equipment;

public interface IEquipmentService
{
    public Task<IReadOnlyList<EquipmentResponse>> GetEquipmentAsync(bool includeInactive, int? projectId, CancellationToken ct);
    public Task<EquipmentResponse?> GetEquipmentByIdAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentResponse>> CreateEquipmentAsync(EquipmentRequest r, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentResponse>> UpdateEquipmentAsync(int id, EquipmentRequest r, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentResponse>> SetEquipmentStatusAsync(int id, bool isActive, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentResponse>> DeleteEquipmentAsync(int id, CancellationToken ct);
}
