using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.CircleByZones;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/circle-by-zones")]
public sealed class CircleByZonesController(ICircleByZoneService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCirclesByZoneAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCircleByZoneAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CircleByZoneRequest request, CancellationToken ct) { var r = await service.CreateCircleByZoneAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This circle already exists in the zone." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.CircleZoneId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CircleByZoneRequest request, CancellationToken ct) { var r = await service.UpdateCircleByZoneAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This circle already exists in the zone." }); return Ok(r.Data); }
}
