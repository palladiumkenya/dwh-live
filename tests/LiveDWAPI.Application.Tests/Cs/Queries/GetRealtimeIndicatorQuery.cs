using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Cs.Queries;

[TestFixture]
public class GetRealtimeIndicatorQueryTests
{
    private IMediator? _mediator;
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase("HIV POSITIVE NOT LINKED")]
    [TestCase("NEGATIVE HIGH-RISK PREGNANT/POSTPARTUM WOMEN NOT LINKED TO PrEP")]
    public async Task should_Read(string name)
    {
        var res =await _mediator.Send(new GetRealtimeIndicatorQuery(name));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
}