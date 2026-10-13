using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.Statuses;

public sealed record StatusRequest([Required, StringLength(50)] string StatusCode, [Required, StringLength(100)] string StatusName, [StringLength(100)] string? ManagementType, bool IsTerminalFinalStatus, [StringLength(100)] string? BadgeVisualStyle, [StringLength(100)] string? Remarks);
public sealed record StatusResponse(int StatusMasterId, string StatusCode, string StatusName, string? ManagementType, bool IsTerminalFinalStatus, string? BadgeVisualStyle, string? Remarks, bool IsActive);
