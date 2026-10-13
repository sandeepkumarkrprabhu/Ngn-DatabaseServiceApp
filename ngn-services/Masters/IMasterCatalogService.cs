namespace ngn_services.Masters;

public interface IMasterCatalogService
{
    Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(bool includeInactive, CancellationToken ct);
    Task<ProjectResponse?> GetProjectAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<ProjectResponse>> CreateProjectAsync(ProjectRequest request, CancellationToken ct);
    Task<MasterOperationResult<ProjectResponse>> UpdateProjectAsync(int id, ProjectRequest request, CancellationToken ct);
    Task<MasterOperationResult<ProjectResponse>> SetProjectStatusAsync(int id, bool isActive, CancellationToken ct);
    Task<MasterOperationResult<ProjectResponse>> DeleteProjectAsync(int id, CancellationToken ct);

    Task<IReadOnlyList<StatusResponse>> GetStatusesAsync(bool includeInactive, CancellationToken ct);
    Task<StatusResponse?> GetStatusAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<StatusResponse>> CreateStatusAsync(StatusRequest request, CancellationToken ct);
    Task<MasterOperationResult<StatusResponse>> UpdateStatusAsync(int id, StatusRequest request, CancellationToken ct);
    Task<MasterOperationResult<StatusResponse>> SetStatusActiveAsync(int id, bool isActive, CancellationToken ct);
    Task<MasterOperationResult<StatusResponse>> DeleteStatusAsync(int id, CancellationToken ct);

    Task<IReadOnlyList<SeverityResponse>> GetSeveritiesAsync(int? projectId, CancellationToken ct);
    Task<SeverityResponse?> GetSeverityAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<SeverityResponse>> CreateSeverityAsync(SeverityRequest request, CancellationToken ct);
    Task<MasterOperationResult<SeverityResponse>> UpdateSeverityAsync(int id, SeverityRequest request, CancellationToken ct);

    Task<IReadOnlyList<EquipmentResponse>> GetEquipmentAsync(bool includeInactive, int? projectId, CancellationToken ct);
    Task<EquipmentResponse?> GetEquipmentByIdAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<EquipmentResponse>> CreateEquipmentAsync(EquipmentRequest request, CancellationToken ct);
    Task<MasterOperationResult<EquipmentResponse>> UpdateEquipmentAsync(int id, EquipmentRequest request, CancellationToken ct);
    Task<MasterOperationResult<EquipmentResponse>> SetEquipmentStatusAsync(int id, bool isActive, CancellationToken ct);
    Task<MasterOperationResult<EquipmentResponse>> DeleteEquipmentAsync(int id, CancellationToken ct);

    Task<IReadOnlyList<EquipmentPartNumberResponse>> GetPartNumbersAsync(bool includeInactive, int? equipmentId, CancellationToken ct);
    Task<EquipmentPartNumberResponse?> GetPartNumberAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<EquipmentPartNumberResponse>> CreatePartNumberAsync(EquipmentPartNumberRequest request, CancellationToken ct);
    Task<MasterOperationResult<EquipmentPartNumberResponse>> UpdatePartNumberAsync(int id, EquipmentPartNumberRequest request, CancellationToken ct);
    Task<MasterOperationResult<EquipmentPartNumberResponse>> SetPartNumberStatusAsync(int id, bool isActive, CancellationToken ct);
    Task<MasterOperationResult<EquipmentPartNumberResponse>> DeletePartNumberAsync(int id, CancellationToken ct);

    Task<IReadOnlyList<FailureSymptomsResponse>> GetFailureSymptomsAsync(int? projectId, CancellationToken ct);
    Task<FailureSymptomsResponse?> GetFailureSymptomAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<FailureSymptomsResponse>> CreateFailureSymptomAsync(FailureSymptomsRequest request, CancellationToken ct);
    Task<MasterOperationResult<FailureSymptomsResponse>> UpdateFailureSymptomAsync(int id, FailureSymptomsRequest request, CancellationToken ct);

    Task<IReadOnlyList<NatureOfProblemResponse>> GetNaturesAsync(int? projectItemId, CancellationToken ct);
    Task<NatureOfProblemResponse?> GetNatureAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<NatureOfProblemResponse>> CreateNatureAsync(NatureOfProblemRequest request, CancellationToken ct);
    Task<MasterOperationResult<NatureOfProblemResponse>> UpdateNatureAsync(int id, NatureOfProblemRequest request, CancellationToken ct);

    Task<IReadOnlyList<CallTypeResponse>> GetCallTypesAsync(CancellationToken ct);
    Task<CallTypeResponse?> GetCallTypeAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<CallTypeResponse>> CreateCallTypeAsync(CallTypeRequest request, CancellationToken ct);
    Task<MasterOperationResult<CallTypeResponse>> UpdateCallTypeAsync(int id, CallTypeRequest request, CancellationToken ct);

    Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct);
    Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<CircleByZoneResponse>> CreateCircleByZoneAsync(CircleByZoneRequest request, CancellationToken ct);
    Task<MasterOperationResult<CircleByZoneResponse>> UpdateCircleByZoneAsync(int id, CircleByZoneRequest request, CancellationToken ct);

    Task<IReadOnlyList<CircleResponse>> GetCirclesAsync(CancellationToken ct);
    Task<CircleResponse?> GetCircleAsync(int id, CancellationToken ct);
    Task<MasterOperationResult<CircleResponse>> CreateCircleAsync(CircleRequest request, CancellationToken ct);
    Task<MasterOperationResult<CircleResponse>> UpdateCircleAsync(int id, CircleRequest request, CancellationToken ct);
}