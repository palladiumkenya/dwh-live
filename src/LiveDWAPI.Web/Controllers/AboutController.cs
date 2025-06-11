using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiveDWAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly IMediator _mediator;

    public AboutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Info")]
    public IActionResult GetInfo()
    {
        return Ok(new{Service="live.dwapi.*",Version="1.0.1",Status="Running"});
    }
}