using System.Runtime.Serialization;
using CSharpFunctionalExtensions;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public enum FilterType
{
    Indicator,Region,Agency,Age,Sex
}
public class GetFiltersQuery:IRequest<Result<object>>
{
    public FilterType Type { get; }

    public GetFiltersQuery(FilterType type)
    {
        Type = type;
    }
}

public class GetFiltersQueryHandler:IRequestHandler<GetFiltersQuery,Result<object>>
{
    private readonly ICsContext _context;

    public GetFiltersQueryHandler(ICsContext context)
    {
        _context = context;
    }

    public async Task<Result<object>> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            object? data = null;
            
            if (request.Type == FilterType.Indicator)
            {
                data = await _context.DimIndicators
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);
                
            }
            if (request.Type == FilterType.Region)
            {
                data = await _context.DimRegions
                    .OrderBy(x => x.County)
                    .ThenBy(x=>x.SubCounty)
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Agency)
            {
                data = await _context.DimAgencies
                    .OrderBy(x => x.Agency)
                    .ThenBy(x=>x.PartnerName)
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Age)
            {
                data = await _context.DimAgeGroups
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Sex)
            {
                data = await _context.DimSex
                    .ToListAsync(cancellationToken);
              
            }
            
            return Result.Success(data);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Filters error!");
            return Result.Failure<Dictionary<Enum, dynamic>>(e.Message);
        }
    }
}