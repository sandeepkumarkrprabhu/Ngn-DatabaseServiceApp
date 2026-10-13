using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.EquipmentPartNumbers;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/equipment-part-numbers")]
public sealed class EquipmentPartNumbersController(IEquipmentPartNumberService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, [FromQuery] int? equipmentId = null, CancellationToken ct = default) => Ok(await service.GetPartNumbersAsync(includeInactive, equipmentId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetPartNumberAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(EquipmentPartNumberRequest request, CancellationToken ct) { var r = await service.CreatePartNumberAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This part number already exists for the equipment." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.EquipmentPartNumberId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, EquipmentPartNumberRequest request, CancellationToken ct) { var r = await service.UpdatePartNumberAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This part number already exists for the equipment." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetPartNumberStatusAsync(id, request.IsActive, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeletePartNumberAsync(id, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return NoContent(); }
}
