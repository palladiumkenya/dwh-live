using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Gf.Queries;

public enum FilterType
{
    Indicator,Region,Agency,Age,Sex
}
public class GetAggregateFiltersQuery:IRequest<Result<object>>
{
    public FilterType Type { get; }

    public GetAggregateFiltersQuery(FilterType type)
    {
        Type = type;
    }
}

public class GetAggregateFiltersQueryHandler:IRequestHandler<GetAggregateFiltersQuery,Result<object>>
{
    private readonly IGfContext _context;

    public GetAggregateFiltersQueryHandler(IGfContext context)
    {
        _context = context;
    }

    public async Task<Result<object>> Handle(GetAggregateFiltersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            object? data = null;

            if (request.Type == FilterType.Indicator)
            {
               data = await _context.DimIndicators
                    .Where(x => x.Name != null)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);

            }

            if (request.Type == FilterType.Region)
            {
                data = await _context.DimRegions
                    .Where(x => x.SubCounty != null)
                    .OrderBy(x => x.County)
                    .ThenBy(x=>x.SubCounty)
                    .ThenBy(x=>x.Ward)
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Agency)
            {
                data = await _context.DimAgencies
                    .Where(x => x.Agency != null)
                    .OrderBy(x => x.Agency)
                    .ThenBy(x=>x.PartnerName)
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Age)
            {
                data = await _context.DimAgeGroups
                    .Where(x => x.AgeGroup != null)
                    .ToListAsync(cancellationToken);
              
            }
            
            if (request.Type == FilterType.Sex)
            {
                data = await _context.DimSex
                    .Where(x => x.Sex != null)
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