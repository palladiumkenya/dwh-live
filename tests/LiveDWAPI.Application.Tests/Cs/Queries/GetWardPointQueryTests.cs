using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LiveDWAPI.Application.Tests.Cs.Queries;

[TestFixture]
public class GetWardPointQueryTests
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
        var res =await _mediator.Send(new GetWardPointQuery(name));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
        foreach (var f in res.Value)
            Log.Information($"{f.County}|{f.SubCounty}|{f.Ward}|{f.Count}|{f.Rate}");
    }
}