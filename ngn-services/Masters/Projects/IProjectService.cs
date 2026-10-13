using ngn_services.Masters;

namespace ngn_services.Masters.Projects;

public interface IProjectService
{
    public Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(bool includeInactive, CancellationToken ct);
    public Task<ProjectResponse?> GetProjectAsync(int id, CancellationToken ct);
    public Task<MasterOperationResult<ProjectResponse>> CreateProjectAsync(ProjectRequest r, CancellationToken ct);
    public Task<MasterOperationResult<ProjectResponse>> UpdateProjectAsync(int id, ProjectRequest r, CancellationToken ct);
    public Task<MasterOperationResult<ProjectResponse>> SetProjectStatusAsync(int id, bool isActive, CancellationToken ct);
    public Task<MasterOperationResult<ProjectResponse>> DeleteProjectAsync(int id, CancellationToken ct);
}
