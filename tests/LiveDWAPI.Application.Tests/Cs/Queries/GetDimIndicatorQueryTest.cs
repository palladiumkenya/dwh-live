using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Cs.Queries;

[TestFixture]
public class GetDimIndicatorQueryTest
{
    
    private IMediator? _mediator;
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task should_Read()
    {
        var res =await _mediator.Send(new GetDimIndicatorQuery());
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
    
}