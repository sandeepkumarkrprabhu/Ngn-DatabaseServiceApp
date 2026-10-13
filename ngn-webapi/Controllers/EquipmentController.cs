using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.Equipment;

namespace ngn_webapi.Controllers;

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
