using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.Equipment;

public sealed record EquipmentRequest([Required, StringLength(100)] string EquipmentName, [StringLength(200)] string? EquipmentDescription, [Range(1, int.MaxValue)] int ProjectId);
public sealed record EquipmentResponse(int EquipmentId, string EquipmentName, string? EquipmentDescription, int ProjectId, string ProjectName, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
