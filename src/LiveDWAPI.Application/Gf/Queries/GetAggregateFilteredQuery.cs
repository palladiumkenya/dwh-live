using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Gf.Dto;
using LiveDWAPI.Domain.Gf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Gf.Queries;

public class GetAggregateFilteredQuery:IRequest<Result<List<FactAggregateIndicator>>>
{
    public FilterGfDto FilterGf { get;  }

    public GetAggregateFilteredQuery(FilterGfDto filterGf)
    {
        FilterGf = filterGf;
    }
}

public class GetAggregateFilteredQueryHandler:IRequestHandler<GetAggregateFilteredQuery,Result<List<FactAggregateIndicator>>>
{
    private readonly IGfContext _context;

    public GetAggregateFilteredQueryHandler(IGfContext context)
    {
        _context = context;
    }

    public async Task<Result<List<FactAggregateIndicator>>> Handle(GetAggregateFilteredQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<FactAggregateIndicator> query = _context.FactAggregateIndicators
                .Where(x=>x.MFLCode!=null);

            // Indicator
            if (request.FilterGf.HasIndicator())
                query = query.Where(x => 
                    x.Indicator != null &&
                    x.Indicator.ToLower()==request.FilterGf.Indicator!.ToLower());

            // Place
            if (request.FilterGf.HasCounty())
                query = query.Where(x =>request.FilterGf.County!.Contains(x.County));
           
            if (request.FilterGf.HasSubCounty())
                query = query.Where(x =>request.FilterGf.SubCounty!.Contains(x.SubCounty));
            
            if (request.FilterGf.HasWard())
                query = query.Where(x =>request.FilterGf.Ward!.Contains(x.Ward));
            
            if (request.FilterGf.HasFacilityName())
                query = query.Where(x =>request.FilterGf.FacilityName!.Contains(x.FacilityName));
            
            // Person
            if (request.FilterGf.HasSex())
                query = query.Where(x =>request.FilterGf.Sex!.Contains(x.Sex));
            
            if (request.FilterGf.HasAgeGroup())
                query = query.Where(x =>request.FilterGf.AgeGroup!.Contains(x.AgeGroup));

            // Partner
            if (request.FilterGf.HasAgency())
                query = query.Where(x =>request.FilterGf.Agency!.Contains(x.Agency));
            
            if (request.FilterGf.HasPartnerName())
                query = query.Where(x =>request.FilterGf.PartnerName!.Contains(x.PartnerName));
         
            var indicators = await query.ToListAsync(cancellationToken);
            
            return Result.Success(indicators);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Aggregate Indicators error!");
            return Result.Failure<List<FactAggregateIndicator>>(e.Message);
        }
    }
}

// public class GetRealtimeFilteredQueryValidator: AbstractValidator<GetRealtimeFilteredQuery>
// {
//     public GetRealtimeFilteredQueryValidator()
//     {
//         RuleFor(x => x.Indicator).NotEmpty()
//             .WithMessage("Indicator is required");
//     }
// }