using LiveDWAPI.Application.Stats.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Stats.Commands;

[TestFixture]
public class UpdateReportingPeriodTests
{
    private IMediator? _mediator;
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }
    [Test]
    public async Task should_Stage_Force()
    {
        var res =await _mediator.Send(new UpdateReportingPeriod(true));
        Assert.That(res.IsSuccess,Is.True);
    }
    [Test]
    public async Task should_Stage()
    {
        var res =await _mediator.Send(new UpdateReportingPeriod(false));
        Assert.That(res.IsSuccess,Is.True);
    }
}
