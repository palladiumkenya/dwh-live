using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CsRealtimePointController : ControllerBase
{
    private readonly IMediator _mediator;
    public CsRealtimePointController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("All")]
    [ProducesResponseType(typeof(IndicatorDataDto), 200)]
    public async Task<IActionResult> GetDataPoints([FromQuery] FilterDto filter)
    {
        try
        {
            var res = await _mediator.Send(new GetRealtimeFilteredQuery(filter));

            if (res.IsSuccess)
            {
                var points = new IndicatorDataDto(res.Value);
                return Ok(points);
            }

            throw new Exception($"Error occured ${res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
}