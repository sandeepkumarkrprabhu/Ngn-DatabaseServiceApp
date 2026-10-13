using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.FailureSymptoms;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/failure-symptoms")]
public sealed class FailureSymptomsController(IFailureSymptomsService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectId, CancellationToken ct) => Ok(await service.GetFailureSymptomsAsync(projectId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetFailureSymptomAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(FailureSymptomsRequest request, CancellationToken ct) { var r = await service.CreateFailureSymptomAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This failure symptom already exists for the project." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.FailureSymptomsId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, FailureSymptomsRequest request, CancellationToken ct) { var r = await service.UpdateFailureSymptomAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This failure symptom already exists for the project." }); return Ok(r.Data); }
}
