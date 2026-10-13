using System.ComponentModel.DataAnnotations;

namespace ngn_services.ProjectGroups;

public sealed record ProjectGroupRequest(
    [Required, StringLength(100, MinimumLength = 1)] string ProjectGroupName,
    [StringLength(250)] string? ProjectGroupDescription);

public sealed record ProjectGroupStatusRequest(bool IsActive);

public sealed record ProjectGroupResponse(
    int ProjectGroupId,
    string ProjectGroupName,
    string? ProjectGroupDescription,
    bool IsActive,
    DateTime CreatedDate,
    DateTime UpdatedDate);

public sealed record ProjectGroupOperationResult(
    ProjectGroupResponse? Data,
    bool NotFound = false,
    bool Conflict = false);