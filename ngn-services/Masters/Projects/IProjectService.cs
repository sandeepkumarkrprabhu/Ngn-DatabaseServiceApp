using ngn_services.Masters;

namespace ngn_services.Masters.Projects;

public interface IProjectService
{
    public async Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(bool includeInactive, CancellationToken ct);
    public async Task<ProjectResponse?> GetProjectAsync(int id, CancellationToken ct);
    public async Task<MasterOperationResult<ProjectResponse>> CreateProjectAsync(ProjectRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<ProjectResponse>> UpdateProjectAsync(int id, ProjectRequest r, CancellationToken ct);
    public async Task<MasterOperationResult<ProjectResponse>> SetProjectStatusAsync(int id, bool isActive, CancellationToken ct);
    public async Task<MasterOperationResult<ProjectResponse>> DeleteProjectAsync(int id, CancellationToken ct);
}
