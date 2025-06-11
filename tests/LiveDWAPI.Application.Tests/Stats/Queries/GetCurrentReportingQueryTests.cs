using LiveDWAPI.Application.Stats.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Stats.Queries;

[TestFixture]
public class GetCurrentReportingQueryTests
{
    
    private IMediator? _mediator;
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task should_Read()
    {
        var res =await _mediator.Send(new GetCurrentReportingQuery());
        Assert.That(res.Value.FacilityCount>0,Is.True);
        Assert.That(res.IsSuccess,Is.True);
    }
    
    [Test]
    public async Task should_Read_Period()
    {
        var lm = DateTime.Now.AddMonths(-1);
        var res =await _mediator.Send(new GetCurrentReportingQuery(new DateTime(lm.Year,lm.Month,01)));
        Assert.That(res.Value.FacilityCount>0,Is.True);
        Assert.That(res.IsSuccess,Is.True);
    }
}