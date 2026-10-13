using ngn_services.Masters;

namespace ngn_services.Masters.Equipment;

public interface IEquipmentService
{
    public async Task<IReadOnlyList<EquipmentResponse>> GetEquipmentAsync(bool includeInactive, int? projectId, CancellationToken ct);
    public async Task<EquipmentResponse?> GetEquipmentByIdAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<EquipmentResponse>> CreateEquipmentAsync(EquipmentRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<EquipmentResponse>> UpdateEquipmentAsync(int id, EquipmentRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<EquipmentResponse>> SetEquipmentStatusAsync(int id, bool isActive, CancellationToken ct);
    public async Task<MasterOperationResult<EquipmentResponse>> DeleteEquipmentAsync(int id, CancellationToken ct);
}
