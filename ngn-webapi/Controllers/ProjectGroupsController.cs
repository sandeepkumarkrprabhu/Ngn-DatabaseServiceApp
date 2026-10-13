using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ngn_data;
using ngn_DbModels.Models;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/project-groups")]
public sealed class ProjectGroupsController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectGroupResponse>>> GetAll(
        [FromQuery] bool includeInactive = false,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.ProjectGroups.AsNoTracking();

        if (!includeInactive)
            query = query.Where(x => x.IsActive);

        var items = await query
            .OrderBy(x => x.ProjectGroupName)
            .Select(x => new ProjectGroupResponse(
                x.ProjectGroupId, x.ProjectGroupName, x.ProjectGroupDescription,
                x.IsActive, x.CreatedDate, x.UpdatedDate))
            .ToListAsync(cancellationToken);

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectGroupResponse>> GetById(
        int id, CancellationToken cancellationToken)
    {
        var item = await dbContext.ProjectGroups.AsNoTracking()
            .Where(x => x.ProjectGroupId == id)
            .Select(x => new ProjectGroupResponse(
                x.ProjectGroupId, x.ProjectGroupName, x.ProjectGroupDescription,
                x.IsActive, x.CreatedDate, x.UpdatedDate))
            .SingleOrDefaultAsync(cancellationToken);

        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectGroupResponse>> Create(
        ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var name = request.ProjectGroupName.Trim();
        var duplicate = await dbContext.ProjectGroups
            .AnyAsync(x => x.ProjectGroupName == name, cancellationToken);

        if (duplicate)
            return Conflict(new ProblemDetails { Title = "A project group with this name already exists." });

        var now = DateTime.UtcNow;
        var entity = new ProjectGroupMaster
        {
            ProjectGroupName = name,
            ProjectGroupDescription = request.ProjectGroupDescription?.Trim(),
            IsActive = true,
            CreatedDate = now,
            UpdatedDate = now
        };

        dbContext.ProjectGroups.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = ToResponse(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.ProjectGroupId }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProjectGroupResponse>> Update(
        int id, ProjectGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups
            .SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);

        if (entity is null)
            return NotFound();

        var name = request.ProjectGroupName.Trim();
        var duplicate = await dbContext.ProjectGroups
            .AnyAsync(x => x.ProjectGroupId != id && x.ProjectGroupName == name, cancellationToken);

        if (duplicate)
            return Conflict(new ProblemDetails { Title = "A project group with this name already exists." });

        entity.ProjectGroupName = name;
        entity.ProjectGroupDescription = request.ProjectGroupDescription?.Trim();
        entity.UpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return Ok(ToResponse(entity));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ProjectGroupResponse>> SetStatus(
        int id, ProjectGroupStatusRequest request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups
            .SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);

        if (entity is null)
            return NotFound();

        if (request.IsActive)
        {
            var hasInactiveParent = false;
            // Project groups have no parent entity; this branch intentionally only reactivates the group.
            if (hasInactiveParent)
                return Conflict(new ProblemDetails { Title = "The project group cannot be activated." });
        }

        entity.IsActive = request.IsActive;
        entity.UpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(ToResponse(entity));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDelete(int id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.ProjectGroups
            .SingleOrDefaultAsync(x => x.ProjectGroupId == id, cancellationToken);

        if (entity is null)
            return NotFound();

        if (!entity.IsActive)
            return NoContent();

        entity.IsActive = false;
        entity.UpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private static ProjectGroupResponse ToResponse(ProjectGroupMaster entity) =>
        new(entity.ProjectGroupId, entity.ProjectGroupName, entity.ProjectGroupDescription,
            entity.IsActive, entity.CreatedDate, entity.UpdatedDate);
}

public sealed record ProjectGroupRequest(
    [Required, StringLength(100, MinimumLength = 1)] string ProjectGroupName,
    [StringLength(250)] string? ProjectGroupDescription);

public sealed record ProjectGroupStatusRequest(bool IsActive);

public sealed record ProjectGroupResponse(
    int ProjectGroupId,
    string ProjectGroupName,
    string? ProjectGroupDescription,
    bool IsActive,
    DateTime CreatedDate,
    DateTime UpdatedDate);
