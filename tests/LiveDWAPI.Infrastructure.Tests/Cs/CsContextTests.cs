using LiveDWAPI.Infrastructure.Cs;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Infrastructure.Tests.Cs;

[TestFixture]
public class CsContextTests
{
    private CsContext? _context;
    [SetUp]
    public void SetUp()
    {
        _context = TestInitializer.ServiceProvider.GetRequiredService<CsContext>();
    }
    
    [Test]
    public void should_Map()
    {
        Assert.That(_context.DimIndicators.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimRegions.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimAgencies.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimAgeGroups.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.DimSex.FirstOrDefault(),Is.Not.Null);
        Assert.That(_context.FactRealtimeIndicators.FirstOrDefault(),Is.Not.Null);
    }
}