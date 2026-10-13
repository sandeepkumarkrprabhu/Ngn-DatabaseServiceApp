using System.ComponentModel.DataAnnotations;

namespace ngn_services.Masters.Projects;

public sealed record ProjectRequest([Required, StringLength(100, MinimumLength = 1)] string ProjectName, [Range(1, int.MaxValue)] int ProjectGroupId);
public sealed record ProjectResponse(int ProjectId, string ProjectName, int ProjectGroupId, string ProjectGroupName, bool IsActive, DateTime CreatedDate, DateTime UpdatedDate);
