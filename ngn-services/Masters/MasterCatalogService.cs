using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;

namespace ngn_services.Masters;

public sealed class MasterCatalogService(AppDbContext db) : IMasterCatalogService
{
    public async Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(bool includeInactive, CancellationToken ct)
    {
        var q = db.ProjectMasters.AsNoTracking();
        if (!includeInactive) q = q.Where(x => x.IsActive);
        var rows = await q.Include(x => x.ProjectGroup).OrderBy(x => x.ProjectName).ToListAsync(ct);
        return rows.Select(ProjectDto).ToList();
    }
    public async Task<ProjectResponse?> GetProjectAsync(int id, CancellationToken ct)
    {
        var x = await db.ProjectMasters.AsNoTracking().Include(x => x.ProjectGroup).SingleOrDefaultAsync(x => x.ProjectId == id, ct);
        return x is null ? null : ProjectDto(x);
    }
    public async Task<MasterOperationResult<ProjectResponse>> CreateProjectAsync(ProjectRequest r, CancellationToken ct)
    {
        var name = r.ProjectName.Trim();
        if (!await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == r.ProjectGroupId && x.IsActive, ct))
            return new(null, Error: "The project group does not exist or is inactive.");
        if (await db.ProjectMasters.AnyAsync(x => x.ProjectName == name, ct)) return new(null, Conflict: true);
        var now = DateTime.UtcNow;
        var e = new ProjectMaster { ProjectName = name, ProjectGroupId = r.ProjectGroupId, IsActive = true, CreatedDate = now, UpdatedDate = now };
        db.ProjectMasters.Add(e); await db.SaveChangesAsync(ct);
        return new(await GetProjectAsync(e.ProjectId, ct));
    }
    public async Task<MasterOperationResult<ProjectResponse>> UpdateProjectAsync(int id, ProjectRequest r, CancellationToken ct)
    {
        var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (!await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == r.ProjectGroupId && x.IsActive, ct))
            return new(null, Error: "The project group does not exist or is inactive.");
        var name = r.ProjectName.Trim();
        if (await db.ProjectMasters.AnyAsync(x => x.ProjectId != id && x.ProjectName == name, ct)) return new(null, Conflict: true);
        e.ProjectName = name; e.ProjectGroupId = r.ProjectGroupId; e.UpdatedDate = DateTime.UtcNow;
        await db.SaveChangesAsync(ct); return new(await GetProjectAsync(id, ct));
    }
    public async Task<MasterOperationResult<ProjectResponse>> SetProjectStatusAsync(int id, bool isActive, CancellationToken ct)
    {
        var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (isActive && !await db.ProjectGroups.AnyAsync(x => x.ProjectGroupId == e.ProjectGroupId && x.IsActive, ct))
            return new(null, Error: "Cannot activate a project while its project group is inactive.");
        e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
        return new(await GetProjectAsync(id, ct));
    }
    public async Task<MasterOperationResult<ProjectResponse>> DeleteProjectAsync(int id, CancellationToken ct)
    {
        var e = await db.ProjectMasters.SingleOrDefaultAsync(x => x.ProjectId == id, ct);
        if (e is null) return new(null, NotFound: true);
        e.IsActive = false; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
        return new(await GetProjectAsync(id, ct));
    }

    public async Task<IReadOnlyList<StatusResponse>> GetStatusesAsync(bool includeInactive, CancellationToken ct)
    {
        var q = db.StatusMasters.AsNoTracking();
        if (!includeInactive) q = q.Where(x => x.IsActive);
        return await q.OrderBy(x => x.StatusName).Select(x => StatusDto(x)).ToListAsync(ct);
    }
    public Task<StatusResponse?> GetStatusAsync(int id, CancellationToken ct) =>
        db.StatusMasters.AsNoTracking().Where(x => x.StatusMasterId == id).Select(x => StatusDto(x)).SingleOrDefaultAsync(ct);
    public async Task<MasterOperationResult<StatusResponse>> CreateStatusAsync(StatusRequest r, CancellationToken ct)
    {
        var code = r.StatusCode.Trim();
        if (await db.StatusMasters.AnyAsync(x => x.StatusCode == code, ct)) return new(null, Conflict: true);
        var e = new StatusMaster { StatusCode = code, StatusName = r.StatusName.Trim(), ManagementType = r.ManagementType?.Trim(), IsTerminalFinalStatus = r.IsTerminalFinalStatus, BadgeVisualStyle = r.BadgeVisualStyle?.Trim(), Remarks = r.Remarks?.Trim(), IsActive = true };
        db.StatusMasters.Add(e); await db.SaveChangesAsync(ct); return new(StatusDto(e));
    }
    public async Task<MasterOperationResult<StatusResponse>> UpdateStatusAsync(int id, StatusRequest r, CancellationToken ct)
    {
        var e = await db.StatusMasters.SingleOrDefaultAsync(x => x.StatusMasterId == id, ct);
        if (e is null) return new(null, NotFound: true);
        var code = r.StatusCode.Trim();
        if (await db.StatusMasters.AnyAsync(x => x.StatusMasterId != id && x.StatusCode == code, ct)) return new(null, Conflict: true);
        e.StatusCode = code; e.StatusName = r.StatusName.Trim(); e.ManagementType = r.ManagementType?.Trim(); e.IsTerminalFinalStatus = r.IsTerminalFinalStatus; e.BadgeVisualStyle = r.BadgeVisualStyle?.Trim(); e.Remarks = r.Remarks?.Trim();
        await db.SaveChangesAsync(ct); return new(StatusDto(e));
    }
    public async Task<MasterOperationResult<StatusResponse>> SetStatusActiveAsync(int id, bool isActive, CancellationToken ct)
    {
        var e = await db.StatusMasters.SingleOrDefaultAsync(x => x.StatusMasterId == id, ct);
        if (e is null) return new(null, NotFound: true);
        e.IsActive = isActive; await db.SaveChangesAsync(ct); return new(StatusDto(e));
    }
    public async Task<MasterOperationResult<StatusResponse>> DeleteStatusAsync(int id, CancellationToken ct) => await SetStatusActiveAsync(id, false, ct);

    public async Task<IReadOnlyList<SeverityResponse>> GetSeveritiesAsync(int? projectId, CancellationToken ct)
    {
        var q = db.SeverityMasters.AsNoTracking();
        if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
        var rows = await q.Include(x => x.Project).OrderBy(x => x.ProjectId).ThenBy(x => x.SeverityName).ToListAsync(ct);
        return rows.Select(SeverityDto).ToList();
    }
    public async Task<SeverityResponse?> GetSeverityAsync(int id, CancellationToken ct)
    {
        var x = await db.SeverityMasters.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.SeverityId == id, ct);
        return x is null ? null : SeverityDto(x);
    }
    public async Task<MasterOperationResult<SeverityResponse>> CreateSeverityAsync(SeverityRequest r, CancellationToken ct)
    {
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        var e = new SeverityMaster { ProjectId = r.ProjectId, SeverityCode = r.SeverityCode.Trim(), SeverityName = r.SeverityName.Trim(), Level = r.Level?.Trim(), ResponseSLA = r.ResponseSLA, ResolutionSLA = r.ResolutionSLA, Description = r.Description?.Trim() };
        db.SeverityMasters.Add(e); await db.SaveChangesAsync(ct); return new(await GetSeverityAsync(e.SeverityId, ct));
    }
    public async Task<MasterOperationResult<SeverityResponse>> UpdateSeverityAsync(int id, SeverityRequest r, CancellationToken ct)
    {
        var e = await db.SeverityMasters.SingleOrDefaultAsync(x => x.SeverityId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        e.ProjectId = r.ProjectId; e.SeverityCode = r.SeverityCode.Trim(); e.SeverityName = r.SeverityName.Trim(); e.Level = r.Level?.Trim(); e.ResponseSLA = r.ResponseSLA; e.ResolutionSLA = r.ResolutionSLA; e.Description = r.Description?.Trim();
        await db.SaveChangesAsync(ct); return new(await GetSeverityAsync(id, ct));
    }

    public async Task<IReadOnlyList<EquipmentResponse>> GetEquipmentAsync(bool includeInactive, int? projectId, CancellationToken ct)
    {
        var q = db.Equipment.AsNoTracking();
        if (!includeInactive) q = q.Where(x => x.IsActive);
        if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
        var rows = await q.Include(x => x.Project).OrderBy(x => x.EquipmentName).ToListAsync(ct);
        return rows.Select(EquipmentDto).ToList();
    }
    public async Task<EquipmentResponse?> GetEquipmentByIdAsync(int id, CancellationToken ct)
    {
        var x = await db.Equipment.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
        return x is null ? null : EquipmentDto(x);
    }
    public async Task<MasterOperationResult<EquipmentResponse>> CreateEquipmentAsync(EquipmentRequest r, CancellationToken ct)
    {
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        var name = r.EquipmentName.Trim();
        if (await db.Equipment.AnyAsync(x => x.ProjectId == r.ProjectId && x.EquipmentName == name, ct)) return new(null, Conflict: true);
        var now = DateTime.UtcNow;
        var e = new EquipmentMaster { EquipmentName = name, EquipmentDescription = r.EquipmentDescription?.Trim(), ProjectId = r.ProjectId, IsActive = true, CreatedDate = now, UpdatedDate = now };
        db.Equipment.Add(e); await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(e.EquipmentId, ct));
    }
    public async Task<MasterOperationResult<EquipmentResponse>> UpdateEquipmentAsync(int id, EquipmentRequest r, CancellationToken ct)
    {
        var e = await db.Equipment.SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        var name = r.EquipmentName.Trim();
        if (await db.Equipment.AnyAsync(x => x.EquipmentId != id && x.ProjectId == r.ProjectId && x.EquipmentName == name, ct)) return new(null, Conflict: true);
        e.EquipmentName = name; e.EquipmentDescription = r.EquipmentDescription?.Trim(); e.ProjectId = r.ProjectId; e.UpdatedDate = DateTime.UtcNow;
        await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(id, ct));
    }
    public async Task<MasterOperationResult<EquipmentResponse>> SetEquipmentStatusAsync(int id, bool isActive, CancellationToken ct)
    {
        var e = await db.Equipment.SingleOrDefaultAsync(x => x.EquipmentId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (isActive && !await db.ProjectMasters.AnyAsync(x => x.ProjectId == e.ProjectId && x.IsActive, ct)) return new(null, Error: "Cannot activate equipment while its project is inactive.");
        e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct); return new(await GetEquipmentByIdAsync(id, ct));
    }
    public async Task<MasterOperationResult<EquipmentResponse>> DeleteEquipmentAsync(int id, CancellationToken ct) => await SetEquipmentStatusAsync(id, false, ct);

    public async Task<IReadOnlyList<EquipmentPartNumberResponse>> GetPartNumbersAsync(bool includeInactive, int? equipmentId, CancellationToken ct)
    {
        var q = db.EquipmentPartNumbers.AsNoTracking();
        if (!includeInactive) q = q.Where(x => x.IsActive);
        if (equipmentId.HasValue) q = q.Where(x => x.EquipmentId == equipmentId.Value);
        var rows = await q.Include(x => x.Equipment).OrderBy(x => x.PartNumber).ToListAsync(ct);
        return rows.Select(PartDto).ToList();
    }
    public async Task<EquipmentPartNumberResponse?> GetPartNumberAsync(int id, CancellationToken ct)
    {
        var x = await db.EquipmentPartNumbers.AsNoTracking().Include(x => x.Equipment).SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
        return x is null ? null : PartDto(x);
    }
    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> CreatePartNumberAsync(EquipmentPartNumberRequest r, CancellationToken ct)
    {
        if (!await db.Equipment.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.IsActive, ct)) return new(null, Error: "The equipment does not exist or is inactive.");
        var part = r.PartNumber.Trim();
        if (await db.EquipmentPartNumbers.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.PartNumber == part, ct)) return new(null, Conflict: true);
        var now = DateTime.UtcNow;
        var e = new EquipmentPartNumber { EquipmentId = r.EquipmentId, PartNumber = part, IsActive = true, CreatedDate = now, UpdatedDate = now };
        db.EquipmentPartNumbers.Add(e); await db.SaveChangesAsync(ct); return new(await GetPartNumberAsync(e.EquipmentPartNumberId, ct));
    }
    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> UpdatePartNumberAsync(int id, EquipmentPartNumberRequest r, CancellationToken ct)
    {
        var e = await db.EquipmentPartNumbers.SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (!await db.Equipment.AnyAsync(x => x.EquipmentId == r.EquipmentId && x.IsActive, ct)) return new(null, Error: "The equipment does not exist or is inactive.");
        var part = r.PartNumber.Trim();
        if (await db.EquipmentPartNumbers.AnyAsync(x => x.EquipmentPartNumberId != id && x.EquipmentId == r.EquipmentId && x.PartNumber == part, ct)) return new(null, Conflict: true);
        e.EquipmentId = r.EquipmentId; e.PartNumber = part; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct);
        return new(await GetPartNumberAsync(id, ct));
    }
    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> SetPartNumberStatusAsync(int id, bool isActive, CancellationToken ct)
    {
        var e = await db.EquipmentPartNumbers.SingleOrDefaultAsync(x => x.EquipmentPartNumberId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (isActive && !await db.Equipment.AnyAsync(x => x.EquipmentId == e.EquipmentId && x.IsActive, ct)) return new(null, Error: "Cannot activate a part number while its equipment is inactive.");
        e.IsActive = isActive; e.UpdatedDate = DateTime.UtcNow; await db.SaveChangesAsync(ct); return new(await GetPartNumberAsync(id, ct));
    }
    public async Task<MasterOperationResult<EquipmentPartNumberResponse>> DeletePartNumberAsync(int id, CancellationToken ct) => await SetPartNumberStatusAsync(id, false, ct);

    public async Task<IReadOnlyList<FailureSymptomsResponse>> GetFailureSymptomsAsync(int? projectId, CancellationToken ct)
    {
        var q = db.FailureSymptoms.AsNoTracking(); if (projectId.HasValue) q = q.Where(x => x.ProjectId == projectId.Value);
        var rows = await q.Include(x => x.Project).OrderBy(x => x.FailureSymptomsName).ToListAsync(ct);
        return rows.Select(FailureDto).ToList();
    }
    public async Task<FailureSymptomsResponse?> GetFailureSymptomAsync(int id, CancellationToken ct)
    {
        var x = await db.FailureSymptoms.AsNoTracking().Include(x => x.Project).SingleOrDefaultAsync(x => x.FailureSymptomsId == id, ct);
        return x is null ? null : FailureDto(x);
    }
    public async Task<MasterOperationResult<FailureSymptomsResponse>> CreateFailureSymptomAsync(FailureSymptomsRequest r, CancellationToken ct)
    {
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        var name = r.FailureSymptomsName.Trim();
        if (await db.FailureSymptoms.AnyAsync(x => x.ProjectId == r.ProjectId && x.FailureSymptomsName == name, ct)) return new(null, Conflict: true);
        var e = new FailureSymptomsMaster { ProjectId = r.ProjectId, FailureSymptomsName = name };
        db.FailureSymptoms.Add(e); await db.SaveChangesAsync(ct); return new(await GetFailureSymptomAsync(e.FailureSymptomsId, ct));
    }
    public async Task<MasterOperationResult<FailureSymptomsResponse>> UpdateFailureSymptomAsync(int id, FailureSymptomsRequest r, CancellationToken ct)
    {
        var e = await db.FailureSymptoms.SingleOrDefaultAsync(x => x.FailureSymptomsId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (!await db.ProjectMasters.AnyAsync(x => x.ProjectId == r.ProjectId && x.IsActive, ct)) return new(null, Error: "The project does not exist or is inactive.");
        var name = r.FailureSymptomsName.Trim();
        if (await db.FailureSymptoms.AnyAsync(x => x.FailureSymptomsId != id && x.ProjectId == r.ProjectId && x.FailureSymptomsName == name, ct)) return new(null, Conflict: true);
        e.ProjectId = r.ProjectId; e.FailureSymptomsName = name; await db.SaveChangesAsync(ct); return new(await GetFailureSymptomAsync(id, ct));
    }

    public async Task<IReadOnlyList<NatureOfProblemResponse>> GetNaturesAsync(int? projectItemId, CancellationToken ct)
    {
        var q = db.NatureOfProblem.AsNoTracking(); if (projectItemId.HasValue) q = q.Where(x => x.ProjectItemId == projectItemId.Value);
        var rows = await q.Include(x => x.ProjectItem).OrderBy(x => x.NatureOfProblemName).ToListAsync(ct);
        return rows.Select(NatureDto).ToList();
    }
    public async Task<NatureOfProblemResponse?> GetNatureAsync(int id, CancellationToken ct)
    {
        var x = await db.NatureOfProblem.AsNoTracking().Include(x => x.ProjectItem).SingleOrDefaultAsync(x => x.NatureOfProblemId == id, ct);
        return x is null ? null : NatureDto(x);
    }
    public async Task<MasterOperationResult<NatureOfProblemResponse>> CreateNatureAsync(NatureOfProblemRequest r, CancellationToken ct)
    {
        if (r.ProjectItemId.HasValue && !await db.ProjectItemDetails.AnyAsync(x => x.ProjectItemId == r.ProjectItemId.Value, ct)) return new(null, Error: "The project item was not found.");
        var name = r.NatureOfProblemName.Trim();
        if (await db.NatureOfProblem.AnyAsync(x => x.ProjectItemId == r.ProjectItemId && x.NatureOfProblemName == name, ct)) return new(null, Conflict: true);
        var e = new NatureOfProblemMaster { NatureOfProblemName = name, ProjectItemId = r.ProjectItemId };
        db.NatureOfProblem.Add(e); await db.SaveChangesAsync(ct); return new(await GetNatureAsync(e.NatureOfProblemId, ct));
    }
    public async Task<MasterOperationResult<NatureOfProblemResponse>> UpdateNatureAsync(int id, NatureOfProblemRequest r, CancellationToken ct)
    {
        var e = await db.NatureOfProblem.SingleOrDefaultAsync(x => x.NatureOfProblemId == id, ct);
        if (e is null) return new(null, NotFound: true);
        if (r.ProjectItemId.HasValue && !await db.ProjectItemDetails.AnyAsync(x => x.ProjectItemId == r.ProjectItemId.Value, ct)) return new(null, Error: "The project item was not found.");
        var name = r.NatureOfProblemName.Trim();
        if (await db.NatureOfProblem.AnyAsync(x => x.NatureOfProblemId != id && x.ProjectItemId == r.ProjectItemId && x.NatureOfProblemName == name, ct)) return new(null, Conflict: true);
        e.NatureOfProblemName = name; e.ProjectItemId = r.ProjectItemId; await db.SaveChangesAsync(ct); return new(await GetNatureAsync(id, ct));
    }

    public async Task<IReadOnlyList<CallTypeResponse>> GetCallTypesAsync(CancellationToken ct) =>
        await db.CallTypeMaster.AsNoTracking().OrderBy(x => x.CallName).Select(x => CallDto(x)).ToListAsync(ct);
    public Task<CallTypeResponse?> GetCallTypeAsync(int id, CancellationToken ct) =>
        db.CallTypeMaster.AsNoTracking().Where(x => x.CallTypeId == id).Select(x => CallDto(x)).SingleOrDefaultAsync(ct);
    public async Task<MasterOperationResult<CallTypeResponse>> CreateCallTypeAsync(CallTypeRequest r, CancellationToken ct)
    {
        var name = r.CallName.Trim();
        if (await db.CallTypeMaster.AnyAsync(x => x.CallName == name, ct)) return new(null, Conflict: true);
        var e = new CallTypeMaster { CallName = name }; db.CallTypeMaster.Add(e); await db.SaveChangesAsync(ct); return new(CallDto(e));
    }
    public async Task<MasterOperationResult<CallTypeResponse>> UpdateCallTypeAsync(int id, CallTypeRequest r, CancellationToken ct)
    {
        var e = await db.CallTypeMaster.SingleOrDefaultAsync(x => x.CallTypeId == id, ct); if (e is null) return new(null, NotFound: true);
        var name = r.CallName.Trim();
        if (await db.CallTypeMaster.AnyAsync(x => x.CallTypeId != id && x.CallName == name, ct)) return new(null, Conflict: true);
        e.CallName = name; await db.SaveChangesAsync(ct); return new(CallDto(e));
    }

    public async Task<IReadOnlyList<CircleByZoneResponse>> GetCirclesByZoneAsync(CancellationToken ct) =>
        await db.CircleByZones.AsNoTracking().OrderBy(x => x.Zone).ThenBy(x => x.CircleName).Select(x => ZoneDto(x)).ToListAsync(ct);
    public Task<CircleByZoneResponse?> GetCircleByZoneAsync(int id, CancellationToken ct) =>
        db.CircleByZones.AsNoTracking().Where(x => x.CircleZoneId == id).Select(x => ZoneDto(x)).SingleOrDefaultAsync(ct);
    public async Task<MasterOperationResult<CircleByZoneResponse>> CreateCircleByZoneAsync(CircleByZoneRequest r, CancellationToken ct)
    {
        var zone = r.Zone.Trim(); var circle = r.CircleName.Trim();
        if (await db.CircleByZones.AnyAsync(x => x.Zone == zone && x.CircleName == circle, ct)) return new(null, Conflict: true);
        var e = new CircleByZoneMaster { Zone = zone, CircleName = circle }; db.CircleByZones.Add(e); await db.SaveChangesAsync(ct); return new(ZoneDto(e));
    }
    public async Task<MasterOperationResult<CircleByZoneResponse>> UpdateCircleByZoneAsync(int id, CircleByZoneRequest r, CancellationToken ct)
    {
        var e = await db.CircleByZones.SingleOrDefaultAsync(x => x.CircleZoneId == id, ct); if (e is null) return new(null, NotFound: true);
        var zone = r.Zone.Trim(); var circle = r.CircleName.Trim();
        if (await db.CircleByZones.AnyAsync(x => x.CircleZoneId != id && x.Zone == zone && x.CircleName == circle, ct)) return new(null, Conflict: true);
        e.Zone = zone; e.CircleName = circle; await db.SaveChangesAsync(ct); return new(ZoneDto(e));
    }

    public async Task<IReadOnlyList<CircleResponse>> GetCirclesAsync(CancellationToken ct) =>
        await db.CircleMaster.AsNoTracking().OrderBy(x => x.CircleName).ThenBy(x => x.CityName).Select(x => CircleDto(x)).ToListAsync(ct);
    public Task<CircleResponse?> GetCircleAsync(int id, CancellationToken ct) =>
        db.CircleMaster.AsNoTracking().Where(x => x.CircleCityId == id).Select(x => CircleDto(x)).SingleOrDefaultAsync(ct);
    public async Task<MasterOperationResult<CircleResponse>> CreateCircleAsync(CircleRequest r, CancellationToken ct)
    {
        var e = new CircleMaster { CircleCode = r.CircleCode.Trim(), CircleName = r.CircleName.Trim(), CityCode = r.CityCode.Trim(), CityName = r.CityName.Trim() };
        db.CircleMaster.Add(e); await db.SaveChangesAsync(ct); return new(CircleDto(e));
    }
    public async Task<MasterOperationResult<CircleResponse>> UpdateCircleAsync(int id, CircleRequest r, CancellationToken ct)
    {
        var e = await db.CircleMaster.SingleOrDefaultAsync(x => x.CircleCityId == id, ct); if (e is null) return new(null, NotFound: true);
        e.CircleCode = r.CircleCode.Trim(); e.CircleName = r.CircleName.Trim(); e.CityCode = r.CityCode.Trim(); e.CityName = r.CityName.Trim();
        await db.SaveChangesAsync(ct); return new(CircleDto(e));
    }

    private static ProjectResponse ProjectDto(ProjectMaster x) => new(x.ProjectId, x.ProjectName, x.ProjectGroupId, x.ProjectGroup.ProjectGroupName, x.IsActive, x.CreatedDate, x.UpdatedDate);
    private static StatusResponse StatusDto(StatusMaster x) => new(x.StatusMasterId, x.StatusCode, x.StatusName, x.ManagementType, x.IsTerminalFinalStatus, x.BadgeVisualStyle, x.Remarks, x.IsActive);
    private static SeverityResponse SeverityDto(SeverityMaster x) => new(x.SeverityId, x.ProjectId, x.Project.ProjectName, x.SeverityCode, x.SeverityName, x.Level, x.ResponseSLA, x.ResolutionSLA, x.Description);
    private static EquipmentResponse EquipmentDto(EquipmentMaster x) => new(x.EquipmentId, x.EquipmentName, x.EquipmentDescription, x.ProjectId, x.Project.ProjectName, x.IsActive, x.CreatedDate, x.UpdatedDate);
    private static EquipmentPartNumberResponse PartDto(EquipmentPartNumber x) => new(x.EquipmentPartNumberId, x.EquipmentId, x.Equipment.EquipmentName, x.PartNumber, x.IsActive, x.CreatedDate, x.UpdatedDate);
    private static FailureSymptomsResponse FailureDto(FailureSymptomsMaster x) => new(x.FailureSymptomsId, x.ProjectId, x.Project.ProjectName, x.FailureSymptomsName);
    private static NatureOfProblemResponse NatureDto(NatureOfProblemMaster x) => new(x.NatureOfProblemId, x.NatureOfProblemName, x.ProjectItemId, x.ProjectItem == null ? null : x.ProjectItem.ItemName);
    private static CallTypeResponse CallDto(CallTypeMaster x) => new(x.CallTypeId, x.CallName);
    private static CircleByZoneResponse ZoneDto(CircleByZoneMaster x) => new(x.CircleZoneId, x.Zone, x.CircleName);
    private static CircleResponse CircleDto(CircleMaster x) => new(x.CircleCityId, x.CircleCode, x.CircleName, x.CityCode, x.CityName);
}
