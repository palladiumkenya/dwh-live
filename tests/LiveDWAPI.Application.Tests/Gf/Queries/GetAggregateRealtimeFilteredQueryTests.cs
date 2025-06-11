using LiveDWAPI.Application.Gf.Dto;
using LiveDWAPI.Application.Gf.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Gf.Queries;

[TestFixture]
public class GetAggregateRealtimeFilteredQueryTests
{
    private IMediator? _mediator;
    
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase("IIT","Nairobi")]
    public async Task should_Read(string name,string county)
    {
        var filter = new FilterGfDto()
        {
            Indicator = name
        };
        var res =await _mediator!.Send(new GetAggregateFilteredQuery(filter));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
   
    [TestCase("AHD","Mombasa","Likoni")]
    public async Task should_Read(string name,string county,string? subcounty)
    {
      var filter = new FilterGfDto()
      {
        Indicator = name
      };
      var res =await _mediator!.Send(new GetAggregateFilteredQuery(filter));
      Assert.That(res.IsSuccess,Is.True);
      Assert.That(res.Value.Any(),Is.True);
    }
}