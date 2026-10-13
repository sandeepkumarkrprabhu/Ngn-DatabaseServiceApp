using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.EquipmentPartNumbers;

public sealed record EquipmentPartNumberRequest([Range(1, int.MaxValue)] int EquipmentId, [Required, StringLength(50)] string PartNumber);
public sealed record EquipmentPartNumberResponse(int EquipmentPartNumberId, int EquipmentId, string EquipmentName, string PartNumber, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
