using Microsoft.AspNetCore.Mvc;
using ngn_services.ProjectGroups;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/project-groups")]
public sealed class ProjectGroupsController(IProjectGroupService projectGroupService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectGroupResponse>>> GetAll(
        [FromQuery] bool includeInactive = false,
        CancellationToken cancellationToken = default)
    {
        var items = await projectGroupService.GetAllAsync(includeInactive, cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectGroupResponse>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var item = await projectGroupService.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectGroupResponse>> Create(
        ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await projectGroupService.CreateAsync(request, cancellationToken);
        if (result.Conflict)
            return Conflict(new ProblemDetails { Title = "A project group with this name already exists." });

        var response = result.Data!;
        return CreatedAtAction(nameof(GetById), new { id = response.ProjectGroupId }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProjectGroupResponse>> Update(
        int id, ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await projectGroupService.UpdateAsync(id, request, cancellationToken);
        if (result.NotFound) return NotFound();
        if (result.Conflict)
            return Conflict(new ProblemDetails { Title = "A project group with this name already exists." });

        return Ok(result.Data);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ProjectGroupResponse>> SetStatus(
        int id, ProjectGroupStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await projectGroupService.SetStatusAsync(id, request, cancellationToken);
        return result.NotFound ? NotFound() : Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDelete(int id, CancellationToken cancellationToken)
    {
        var result = await projectGroupService.SoftDeleteAsync(id, cancellationToken);
        return result.NotFound ? NotFound() : NoContent();
    }
}
