namespace ngn_services.ProjectGroups;

public interface IProjectGroupService
{
    Task<IReadOnlyList<ProjectGroupResponse>> GetAllAsync(bool includeInactive, CancellationToken cancellationToken);
    Task<ProjectGroupResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProjectGroupOperationResult> CreateAsync(ProjectGroupRequest request, CancellationToken cancellationToken);
    Task<ProjectGroupOperationResult> UpdateAsync(int id, ProjectGroupRequest request, CancellationToken cancellationToken);
    Task<ProjectGroupOperationResult> SetStatusAsync(int id, ProjectGroupStatusRequest request, CancellationToken cancellationToken);
    Task<ProjectGroupOperationResult> SoftDeleteAsync(int id, CancellationToken cancellationToken);
}