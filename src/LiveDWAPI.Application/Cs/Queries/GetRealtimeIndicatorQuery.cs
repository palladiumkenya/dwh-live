using CSharpFunctionalExtensions;
using FluentValidation;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetRealtimeIndicatorQuery:IRequest<Result<List<FactRealtimeIndicator>>>
{
    public string Indicator { get;  }

    public GetRealtimeIndicatorQuery(string indicator)
    {
        Indicator = indicator;
    }
}

public class GetRealtimeIndicatorQueryHandler:IRequestHandler<GetRealtimeIndicatorQuery,Result<List<FactRealtimeIndicator>>>
{
    private readonly ICsContext _context;

    public GetRealtimeIndicatorQueryHandler(ICsContext context)
    {
        _context = context;
    }

    public async Task<Result<List<FactRealtimeIndicator>>> Handle(GetRealtimeIndicatorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            
            var indicators =await  _context.FactRealtimeIndicators
                .Where(x=>
                    x.Indicator != null && 
                    x.Indicator.ToLower()==request.Indicator.ToLower())
                .ToListAsync(cancellationToken);

            return Result.Success(indicators);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<List<FactRealtimeIndicator>>(e.Message);
        }
    }
}

public class GetRealtimeIndicatorQueryValidator: AbstractValidator<GetRealtimeIndicatorQuery>
{
    public GetRealtimeIndicatorQueryValidator()
    {
        RuleFor(x => x.Indicator).NotEmpty()
            .WithMessage("Indicator is required");
    }
}