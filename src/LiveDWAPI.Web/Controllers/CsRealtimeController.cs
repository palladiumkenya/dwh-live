using LiveDWAPI.Application.Cs.Queries;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CsRealtimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CsRealtimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Indicator")]
    [ProducesResponseType(typeof(List<DimIndicator>), 200)]
    public async Task<IActionResult> GetIndicator()
    {
        try
        {
            var res = await _mediator.Send(new GetDimIndicatorQuery());
    
            if (res.IsSuccess)
                return Ok(res.Value); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("IndicatorData")]
    [ProducesResponseType(typeof(List<FactRealtimeIndicator>), 200)]
    public async Task<IActionResult> GetData(string indicator)
    {
        try
        {
            var res = await _mediator.Send(new GetRealtimeIndicatorQuery(indicator));
    
            if (res.IsSuccess)
                return Ok(res.Value); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error loading");
            return StatusCode(500, e.Message);
        }
    }
}