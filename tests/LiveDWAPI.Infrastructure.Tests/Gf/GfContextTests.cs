using LiveDWAPI.Infrastructure.Gf;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Infrastructure.Tests.Gf;

[TestFixture]
public class GfContextTests
{
    private GfContext? _context;
    [SetUp]
    public void SetUp()
    {
        _context = TestInitializer.ServiceProvider.GetRequiredService<GfContext>();
        _context.Initialize();
    }
    
    [Test]
    public void should_Map()
    {
        Assert.That(_context.DimIndicators.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimRegions.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimAgencies.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimAgeGroups.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimSex.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.FactAggregateIndicators.FirstOrDefault(),Is.Not.Null);
    }
}