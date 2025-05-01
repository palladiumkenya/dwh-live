using CSharpFunctionalExtensions;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetDimIndicatorQuery:IRequest<Result<List<DimIndicator>>>
{
    public GetDimIndicatorQuery()
    {
    }
}

public class GetDimIndicatorQueryHandler:IRequestHandler<GetDimIndicatorQuery,Result<List<DimIndicator>>>
{
    private readonly ICsContext _context;

    public GetDimIndicatorQueryHandler(ICsContext context)
    {
        _context = context;
    }

    public async Task<Result<List<DimIndicator>>> Handle(GetDimIndicatorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            
            var indicators =await  _context.DimIndicators
                .OrderByDescending(x=>x.Name)
                .ToListAsync(cancellationToken);

            return Result.Success(indicators);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Indicators error!");
            return Result.Failure<List<DimIndicator>>(e.Message);
        }
    }
}