using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/projects")]
public sealed class ProjectsController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, CancellationToken ct = default) => Ok(await service.GetProjectsAsync(includeInactive, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetProjectAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(ProjectRequest request, CancellationToken ct) { var r = await service.CreateProjectAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A project with this name already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.ProjectId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, ProjectRequest request, CancellationToken ct) { var r = await service.UpdateProjectAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A project with this name already exists." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetProjectStatusAsync(id, request.IsActive, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeleteProjectAsync(id, ct); return r.NotFound ? NotFound() : NoContent(); }
}

[ApiController]
[Route("api/statuses")]
public sealed class StatusesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, CancellationToken ct = default) => Ok(await service.GetStatusesAsync(includeInactive, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetStatusAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(StatusRequest request, CancellationToken ct) { var r = await service.CreateStatusAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A status with this code already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.StatusMasterId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, StatusRequest request, CancellationToken ct) { var r = await service.UpdateStatusAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A status with this code already exists." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetStatusActiveAsync(id, request.IsActive, ct); return r.NotFound ? NotFound() : Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeleteStatusAsync(id, ct); return r.NotFound ? NotFound() : NoContent(); }
}

[ApiController]
[Route("api/severities")]
public sealed class SeveritiesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectId, CancellationToken ct) => Ok(await service.GetSeveritiesAsync(projectId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetSeverityAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(SeverityRequest request, CancellationToken ct) { var r = await service.CreateSeverityAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); return CreatedAtAction(nameof(Get), new { id = r.Data!.SeverityId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, SeverityRequest request, CancellationToken ct) { var r = await service.UpdateSeverityAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
}

[ApiController]
[Route("api/equipment")]
public sealed class EquipmentController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] int? projectId = null, CancellationToken ct = default) => Ok(await service.GetEquipmentAsync(includeInactive, projectId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetEquipmentByIdAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(EquipmentRequest request, CancellationToken ct) { var r = await service.CreateEquipmentAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "Equipment with this name already exists in the project." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.EquipmentId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, EquipmentRequest request, CancellationToken ct) { var r = await service.UpdateEquipmentAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "Equipment with this name already exists in the project." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetEquipmentStatusAsync(id, request.IsActive, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeleteEquipmentAsync(id, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return NoContent(); }
}

[ApiController]
[Route("api/equipment-part-numbers")]
public sealed class EquipmentPartNumbersController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] int? equipmentId = null, CancellationToken ct = default) => Ok(await service.GetPartNumbersAsync(includeInactive, equipmentId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetPartNumberAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(EquipmentPartNumberRequest request, CancellationToken ct) { var r = await service.CreatePartNumberAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This part number already exists for the equipment." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.EquipmentPartNumberId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, EquipmentPartNumberRequest request, CancellationToken ct) { var r = await service.UpdatePartNumberAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This part number already exists for the equipment." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetPartNumberStatusAsync(id, request.IsActive, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeletePartNumberAsync(id, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return NoContent(); }
}

[ApiController]
[Route("api/failure-symptoms")]
public sealed class FailureSymptomsController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectId, CancellationToken ct) => Ok(await service.GetFailureSymptomsAsync(projectId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetFailureSymptomAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(FailureSymptomsRequest request, CancellationToken ct) { var r = await service.CreateFailureSymptomAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This failure symptom already exists for the project." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.FailureSymptomsId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, FailureSymptomsRequest request, CancellationToken ct) { var r = await service.UpdateFailureSymptomAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This failure symptom already exists for the project." }); return Ok(r.Data); }
}

[ApiController]
[Route("api/nature-of-problems")]
public sealed class NatureOfProblemsController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectItemId, CancellationToken ct) => Ok(await service.GetNaturesAsync(projectItemId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetNatureAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(NatureOfProblemRequest request, CancellationToken ct) { var r = await service.CreateNatureAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This nature of problem already exists for the project item." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.NatureOfProblemId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, NatureOfProblemRequest request, CancellationToken ct) { var r = await service.UpdateNatureAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This nature of problem already exists for the project item." }); return Ok(r.Data); }
}

[ApiController]
[Route("api/call-types")]
public sealed class CallTypesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCallTypesAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCallTypeAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CallTypeRequest request, CancellationToken ct) { var r = await service.CreateCallTypeAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A call type with this name already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.CallTypeId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CallTypeRequest request, CancellationToken ct) { var r = await service.UpdateCallTypeAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A call type with this name already exists." }); return Ok(r.Data); }
}

[ApiController]
[Route("api/circle-by-zones")]
public sealed class CircleByZonesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCirclesByZoneAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCircleByZoneAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CircleByZoneRequest request, CancellationToken ct) { var r = await service.CreateCircleByZoneAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This circle already exists in the zone." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.CircleZoneId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CircleByZoneRequest request, CancellationToken ct) { var r = await service.UpdateCircleByZoneAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This circle already exists in the zone." }); return Ok(r.Data); }
}

[ApiController]
[Route("api/circles")]
public sealed class CirclesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCirclesAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCircleAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CircleRequest request, CancellationToken ct) { var r = await service.CreateCircleAsync(request, ct); return CreatedAtAction(nameof(Get), new { id = r.Data!.CircleCityId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CircleRequest request, CancellationToken ct) { var r = await service.UpdateCircleAsync(id, request, ct); return r.NotFound ? NotFound() : Ok(r.Data); }
}
