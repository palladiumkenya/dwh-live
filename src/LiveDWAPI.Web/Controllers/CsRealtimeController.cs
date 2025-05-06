using AutoMapper;
using LiveDWAPI.Application.Cs.Dto;
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
    private readonly IMapper _mapper;

    public CsRealtimeController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Indicator")]
    [ProducesResponseType(typeof(List<string>), 200)]
    public async Task<IActionResult> GetIndicator()
    {
        try
        {
            var res = await _mediator.Send(new GetDimIndicatorQuery());

            if (res.IsSuccess)
                return Ok(res.Value.Select(x=>x.Name));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet("IndicatorData")]
    [ProducesResponseType(typeof(FactRealtimeIndicator), 200)]
    public async Task<IActionResult> GetData([FromQuery] FilterDto filter)
    {
        try
        {
            var res = await _mediator.Send(new GetRealtimeFilteredQuery(filter));

            if (res.IsSuccess)
                return Ok(res.Value);
            
            throw new Exception("Error occured");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("Points")]
    [ProducesResponseType(typeof(IndicatorDataDto), 200)]
    public async Task<IActionResult> GetData(string indicator)
    {
        try
        {
            var resC = await _mediator.Send(new GetCountyPointQuery(indicator));
            var resS = await _mediator.Send(new GetSubCountyPointQuery(indicator));
            var resF = await _mediator.Send(new GetFacilityPointQuery(indicator));

            if (resC.IsSuccess && resS.IsSuccess && resF.IsSuccess)
                return Ok(new IndicatorDataDto()
                {
                    CountyPoints = resC.Value,
                    SubCountyPoints = resS.Value,
                    FacilityPoints = resF.Value
                });

            if (resC.IsFailure)
                throw new Exception($"An error occured:{resC.Error}");
            if (resS.IsFailure)
                throw new Exception($"An error occured:{resS.Error}");
            if (resF.IsFailure)
                throw new Exception($"An error occured:{resF.Error}");

            throw new Exception("Error occured");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Points/Facility")]
    [ProducesResponseType(typeof(List<FactFacilityPointDto>), 200)]
    public async Task<IActionResult> GetFacilityData(string indicator)
    {
        try
        {
            var resF = await _mediator.Send(new GetFacilityPointQuery(indicator));

            if (resF.IsSuccess)
                return Ok(resF.Value);

            throw new Exception($"An error occured:{resF.Error}");

        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Points/County")]
    [ProducesResponseType(typeof(List<FactCountyPointDto>), 200)]
    public async Task<IActionResult> GetCountyData(string indicator)
    {
        try
        {
            var resF = await _mediator.Send(new GetCountyPointQuery(indicator));

            if (resF.IsSuccess)
                return Ok(resF.Value);

            throw new Exception($"An error occured:{resF.Error}");

        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Points/SubCounty")]
    [ProducesResponseType(typeof(List<FactSubCountyPointDto>), 200)]
    public async Task<IActionResult> GetSubCountyData(string indicator)
    {
        try
        {
            var resF = await _mediator.Send(new GetSubCountyPointQuery(indicator));

            if (resF.IsSuccess)
                return Ok(resF.Value);

            throw new Exception($"An error occured:{resF.Error}");

        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
}