using LiveDWAPI.Application.Cs.Queries;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CsFilterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CsFilterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Indicator")]
    [ProducesResponseType(typeof(List<DimIndicator>), 200)]
    public async Task<IActionResult> GetIndicator()
    {
        try
        {
            var res = await _mediator.Send(new GetFiltersQuery(FilterType.Indicator));

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Region")]
    [ProducesResponseType(typeof(List<DimRegion>), 200)]
    public async Task<IActionResult> GetRegions()
    {
        try
        {
            var res = await _mediator.Send(new GetFiltersQuery(FilterType.Region));

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Agency")]
    [ProducesResponseType(typeof(List<DimAgency>), 200)]
    public async Task<IActionResult> GetAgencies()
    {
        try
        {
            var res = await _mediator.Send(new GetFiltersQuery(FilterType.Agency));

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Sex")]
    [ProducesResponseType(typeof(List<DimSex>), 200)]
    public async Task<IActionResult> GetSex()
    {
        try
        {
            var res = await _mediator.Send(new GetFiltersQuery(FilterType.Sex));

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Age")]
    [ProducesResponseType(typeof(List<DimAgeGroup>), 200)]
    public async Task<IActionResult> GetAge()
    {
        try
        {
            var res = await _mediator.Send(new GetFiltersQuery(FilterType.Age));

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
}