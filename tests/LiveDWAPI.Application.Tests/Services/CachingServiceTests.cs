using LiveDWAPI.Application.Cs.Queries;
using LiveDWAPI.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Services;

public class CachingServiceTests
{
    private ICachingService _service;
    private IMediator _mediator;

    [SetUp]
    public void SetUp()
    {
        _service = TestInitializer.ServiceProvider.GetRequiredService<ICachingService>();
    }
    
    [Test]
    public async Task should_LoadFromCache()
    {
        var req = new GetFiltersQuery(FilterType.Indicator);
        
        var res =await _service.LoadFromCache("Cs.Filter.Indicator", req);
        Assert.That(res,Is.Not.Null);
        
        var res2 =await _service.LoadFromCache("Cs.Filter.Indicator", req);
        Assert.That(res2,Is.Not.Null);
    }
}