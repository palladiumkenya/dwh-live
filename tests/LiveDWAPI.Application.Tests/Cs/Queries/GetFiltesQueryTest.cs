using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace LiveDWAPI.Application.Tests.Cs.Queries;

[TestFixture]
public class GetFiltersQueryTest
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
        var res =await _mediator.Send(new GetFiltersQuery(ftype));
        Assert.That(res.IsSuccess,Is.True);
        Log.Information(JsonConvert.SerializeObject(res.Value));
    }
}