using LiveDWAPI.Application.Gf.Dto;
using LiveDWAPI.Application.Gf.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GfAggregatePointController : ControllerBase
{
    private readonly IMediator _mediator;
    public GfAggregatePointController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("All")]
    [ProducesResponseType(typeof(IndicatorGfDataDto), 200)]
    public async Task<IActionResult> GetDataPointsByQuery([FromQuery] FilterGfDto filterGf)
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFilteredQuery(filterGf));

            if (res.IsSuccess)
            {
                var points = new IndicatorGfDataDto(res.Value);
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
    
    [HttpPost("All")]
    [ProducesResponseType(typeof(IndicatorGfDataDto), 200)]
    public async Task<IActionResult> GetDataPointsByBody([FromBody] FilterGfDto filterGf)
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFilteredQuery(filterGf));

            if (res.IsSuccess)
            {
                var points = new IndicatorGfDataDto(res.Value);
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