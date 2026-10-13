using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.Severities;

public sealed record SeverityRequest([Range(1, int.MaxValue)] int ProjectId, [Required, StringLength(10)] string SeverityCode, [Required, StringLength(50)] string SeverityName, [StringLength(30)] string? Level, [Range(0, 99999999)] decimal ResponseSLA, [Range(0, 99999999)] decimal ResolutionSLA, [StringLength(100)] string? Description);
public sealed record SeverityResponse(int SeverityId, int ProjectId, string ProjectName, string SeverityCode, string SeverityName, string? Level, decimal ResponseSLA, decimal ResolutionSLA, string? Description);
