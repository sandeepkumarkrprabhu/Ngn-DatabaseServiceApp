using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.Statuses;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/statuses")]
public sealed class StatusesController(IStatusService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, CancellationToken ct = default) => Ok(await service.GetStatusesAsync(includeInactive, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetStatusAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(StatusRequest request, CancellationToken ct) { var r = await service.CreateStatusAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A status with this code already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.StatusMasterId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, StatusRequest request, CancellationToken ct) { var r = await service.UpdateStatusAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A status with this code already exists." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetStatusActiveAsync(id, request.IsActive, ct); return r.NotFound ? NotFound() : Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeleteStatusAsync(id, ct); return r.NotFound ? NotFound() : NoContent(); }
}
