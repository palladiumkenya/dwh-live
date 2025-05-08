using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Application.Stats.Commands;
using LiveDWAPI.Application.Stats.Dto;
using LiveDWAPI.Application.Stats.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IMediator _mediator;
    public StatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Reporting/Current")]
    [ProducesResponseType(typeof(CurrentReportingDto), 200)]
    public async Task<IActionResult> GetSiteReport()
    {
        try
        {
            var res = await _mediator.Send(new GetCurrentReportingQuery());

            if (res.IsSuccess)
                return Ok(res.Value);
            
            throw new Exception($"Error occured ${res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Reporting/Refresh")]
    public async Task<IActionResult> UpdateReportingPeriod()
    {
        try
        {
            var res = await _mediator.Send(new UpdateReportingPeriod(false));

            if (res.IsSuccess)
                return Ok(new {Status="Updated!"});

            throw new Exception($"Error occured ${res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("Reporting/Refresh/Force")]
    public async Task<IActionResult> ForceUpdateReportingPeriod()
    {
        try
        {
            var res = await _mediator.Send(new UpdateReportingPeriod(true));

            if (res.IsSuccess)
                return Ok(new {Status="Force Updated!"});

            throw new Exception($"Error occured ${res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
}