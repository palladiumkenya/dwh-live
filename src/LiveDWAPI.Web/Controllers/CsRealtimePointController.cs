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
public class CsRealtimePointController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CsRealtimePointController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
                var points = new IndicatorDataDto(res.Value, _mapper);
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