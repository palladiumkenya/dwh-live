using LiveDWAPI.Application.Gf.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace LiveDWAPI.Application.Tests.Gf.Queries;

[TestFixture]
public class GetAggregateFiltersQueryTest
{
    
    private IMediator? _mediator;
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase(FilterType.Indicator)]
    public async Task should_Read(FilterType ftype)
    {
        var res =await _mediator.Send(new GetAggregateFiltersQuery(ftype));
        Assert.That(res.IsSuccess,Is.True);
        Log.Information(JsonConvert.SerializeObject(res.Value));
    }
}