using LiveDWAPI.Application.Stats.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Infrastructure.Tests.Stats;

[TestFixture]
public class StatsServiceTests
{
    private IStatsService _service;
    
    
    [SetUp]
    public void SetUp()
    {
        _service = TestInitializer.ServiceProvider.GetRequiredService<IStatsService>();
    }
    
    [Test]
    public async Task should_LoadCurrent()
    {
        var rep =await _service.LoadCurrent();
        Assert.That(rep.Any,Is.True);
    }
}