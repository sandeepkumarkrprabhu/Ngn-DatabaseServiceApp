using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.CallTypes;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/call-types")]
public sealed class CallTypesController(ICallTypeService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCallTypesAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCallTypeAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CallTypeRequest request, CancellationToken ct) { var r = await service.CreateCallTypeAsync(request, ct); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A call type with this name already exists." }); return CreatedAtAction(nameof(Get), new { id = r.Data!.CallTypeId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CallTypeRequest request, CancellationToken ct) { var r = await service.UpdateCallTypeAsync(id, request, ct); if (r.NotFound) return NotFound(); if (r.Conflict) return Conflict(new ProblemDetails { Title = "A call type with this name already exists." }); return Ok(r.Data); }
}
