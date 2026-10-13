using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters;

public sealed record ProjectRequest([Required, StringLength(100, MinimumLength = 1)] string ProjectName, [Range(1, int.MaxValue)] int ProjectGroupId);
public sealed record ProjectResponse(int ProjectId, string ProjectName, int ProjectGroupId, string ProjectGroupName, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
public sealed record StatusRequest([Required, StringLength(50)] string StatusCode, [Required, StringLength(100)] string StatusName, [StringLength(100)] string? ManagementType, bool IsTerminalFinalStatus, [StringLength(100)] string? BadgeVisualStyle, [StringLength(100)] string? Remarks);
public sealed record StatusStatusRequest(bool IsActive);
public sealed record StatusResponse(int StatusMasterId, string StatusCode, string StatusName, string? ManagementType, bool IsTerminalFinalStatus, string? BadgeVisualStyle, string? Remarks, bool IsActive);
public sealed record SeverityRequest([Range(1, int.MaxValue)] int ProjectId, [Required, StringLength(10)] string SeverityCode, [Required, StringLength(50)] string SeverityName, [StringLength(30)] string? Level, [Range(0, 99999999)] decimal ResponseSLA, [Range(0, 99999999)] decimal ResolutionSLA, [StringLength(100)] string? Description);
public sealed record SeverityResponse(int SeverityId, int ProjectId, string ProjectName, string SeverityCode, string SeverityName, string? Level, decimal ResponseSLA, decimal ResolutionSLA, string? Description);
public sealed record EquipmentRequest([Required, StringLength(100)] string EquipmentName, [StringLength(200)] string? EquipmentDescription, [Range(1, int.MaxValue)] int ProjectId);
public sealed record EquipmentStatusRequest(bool IsActive);
public sealed record EquipmentResponse(int EquipmentId, string EquipmentName, string? EquipmentDescription, int ProjectId, string ProjectName, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
public sealed record EquipmentPartNumberRequest([Range(1, int.MaxValue)] int EquipmentId, [Required, StringLength(50)] string PartNumber);
public sealed record EquipmentPartNumberStatusRequest(bool IsActive);
public sealed record EquipmentPartNumberResponse(int EquipmentPartNumberId, int EquipmentId, string EquipmentName, string PartNumber, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
public sealed record FailureSymptomsRequest([Range(1, int.MaxValue)] int ProjectId, [Required, StringLength(200)] string FailureSymptomsName);
public sealed record FailureSymptomsResponse(int FailureSymptomsId, int ProjectId, string ProjectName, string FailureSymptomsName);
public sealed record NatureOfProblemRequest([Required, StringLength(200)] string NatureOfProblemName, int? ProjectItemId);
public sealed record NatureOfProblemResponse(int NatureOfProblemId, string NatureOfProblemName, int? ProjectItemId, string? ProjectItemName);
public sealed record CallTypeRequest([Required, StringLength(50)] string CallName);
public sealed record CallTypeResponse(int CallTypeId, string CallName);
public sealed record CircleByZoneRequest([Required, StringLength(50)] string Zone, [Required, StringLength(100)] string CircleName);
public sealed record CircleByZoneResponse(int CircleZoneId, string Zone, string CircleName);
public sealed record CircleRequest([Required, StringLength(50)] string CircleCode, [Required, StringLength(100)] string CircleName, [Required, StringLength(50)] string CityCode, [Required, StringLength(100)] string CityName);
public sealed record CircleResponse(int CircleCityId, string CircleCode, string CircleName, string CityCode, string CityName);
public sealed record MasterOperationResult<T>(T? Data, bool NotFound = false, bool Conflict = false, string? Error = null);