using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.Severities;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/severities")]
public sealed class SeveritiesController(IMasterCatalogService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectId, CancellationToken ct) => Ok(await service.GetSeveritiesAsync(projectId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetSeverityAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(SeverityRequest request, CancellationToken ct) { var r = await service.CreateSeverityAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); return CreatedAtAction(nameof(Get), new { id = r.Data!.SeverityId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, SeverityRequest request, CancellationToken ct) { var r = await service.UpdateSeverityAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
}
