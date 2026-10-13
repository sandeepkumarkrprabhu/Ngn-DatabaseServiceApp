using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.Projects;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/projects")]
public sealed class ProjectsController(IProjectService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false, CancellationToken ct = default) => Ok(await service.GetProjectsAsync(includeInactive, ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetProjectAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(ProjectRequest request, CancellationToken ct) { var r = await service.CreateProjectAsync(request, ct); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A project with this name already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.ProjectId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, ProjectRequest request, CancellationToken ct) { var r = await service.UpdateProjectAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return BadRequest(new ProblemDetails { Title = r.Error }); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A project with this name already exists." }); return Ok(r.Data); }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> SetStatus(int id, StatusChangeRequest request, CancellationToken ct) { var r = await service.SetProjectStatusAsync(id, request.IsActive, ct); if (r.NotFound) return NotFound(); if (r.Error is not null) return Conflict(new ProblemDetails { Title = r.Error }); return Ok(r.Data); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { var r = await service.DeleteProjectAsync(id, ct); return r.NotFound ? NotFound() : NoContent(); }
}
