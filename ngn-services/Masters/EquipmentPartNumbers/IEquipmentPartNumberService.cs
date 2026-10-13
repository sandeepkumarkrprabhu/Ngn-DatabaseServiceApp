using ngn_services.Masters;

namespace ngn_services.Masters.EquipmentPartNumbers;

public interface IEquipmentPartNumberService
{
    public Task<IReadOnlyList<EquipmentPartNumberResponse>> GetPartNumbersAsync(bool includeInactive, int? equipmentId, CancellationToken ct);
    public Task<EquipmentPartNumberResponse?> GetPartNumberAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentPartNumberResponse>> CreatePartNumberAsync(EquipmentPartNumberRequest r, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentPartNumberResponse>> UpdatePartNumberAsync(int id, EquipmentPartNumberRequest r, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentPartNumberResponse>> SetPartNumberStatusAsync(int id, bool isActive, CancellationToken ct);
    public Task<MasterOperationResult<EquipmentPartNumberResponse>> DeletePartNumberAsync(int id, CancellationToken ct);
}
