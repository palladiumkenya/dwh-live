using AutoMapper;
using LiveDWAPI.Application.Gf.Dto;
using LiveDWAPI.Application.Gf.Queries;
using LiveDWAPI.Domain.Gf;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GfFilterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GfFilterController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("Indicator")]
    [ProducesResponseType(typeof(List<DimIndicatorGf>), 200)]
    public async Task<IActionResult> GetIndicator()
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFiltersQuery(FilterType.Indicator));

            if (res.IsSuccess)
                return Ok(_mapper.Map<List<DimIndicatorGf>>(res.Value));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Region")]
    [ProducesResponseType(typeof(List<DimRegionGf>), 200)]
    public async Task<IActionResult> GetRegions()
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFiltersQuery(FilterType.Region));

            if (res.IsSuccess)
                return Ok(_mapper.Map<List<DimRegionGf>>(res.Value));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Agency")]
    [ProducesResponseType(typeof(List<DimAgencyGf>), 200)]
    public async Task<IActionResult> GetAgencies()
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFiltersQuery(FilterType.Agency));

            if (res.IsSuccess)
                return Ok(_mapper.Map<List<DimAgencyGf>>(res.Value));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Sex")]
    [ProducesResponseType(typeof(List<DimSexGf>), 200)]
    public async Task<IActionResult> GetSex()
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFiltersQuery(FilterType.Sex));

            if (res.IsSuccess)
                return Ok(_mapper.Map<List<DimSexGf>>(res.Value));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Age")]
    [ProducesResponseType(typeof(List<DimAgeGroupGf>), 200)]
    public async Task<IActionResult> GetAge()
    {
        try
        {
            var res = await _mediator.Send(new GetAggregateFiltersQuery(FilterType.Age));

            if (res.IsSuccess)
                return Ok(_mapper.Map<List<DimAgeGroupGf>>(res.Value));

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
}