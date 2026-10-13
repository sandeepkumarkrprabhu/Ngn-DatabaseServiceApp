using Microsoft.AspNetCore.Mvc;
using ngn_services.Masters;
using ngn_services.Masters.Circles;

namespace ngn_webapi.Controllers;

[ApiController]
[Route("api/circles")]
public sealed class CirclesController(ICircleService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetCirclesAsync(ct));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id, CancellationToken ct) { var x = await service.GetCircleAsync(id, ct); return x is null ? NotFound() : Ok(x); }
    [HttpPost] public async Task<IActionResult> Create(CircleRequest request, CancellationToken ct) { var r = await service.CreateCircleAsync(request, ct); return CreatedAtAction(nameof(Get), new { id = r.Data!.CircleCityId }, r.Data); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, CircleRequest request, CancellationToken ct) { var r = await service.UpdateCircleAsync(id, request, ct); return r.NotFound ? NotFound() : Ok(r.Data); }
}
