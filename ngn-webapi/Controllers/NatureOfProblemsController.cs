using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters.NatureOfProblems;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/nature-of-problems")]
public sealed class NatureOfProblemsController(INatureOfProblemService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? projectItemId, CancellationToken ct) => Ok(await service.GetNaturesAsync(projectItemId, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetNatureAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(NatureOfProblemRequest request, CancellationToken ct) { var r = await service.CreateNatureAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This nature of problem already exists for the project item." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.NatureOfProblemId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, NatureOfProblemRequest request, CancellationToken ct) { var r = await service.UpdateNatureAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "This nature of problem already exists for the project item." }); return Ok(r.Data); }
}
