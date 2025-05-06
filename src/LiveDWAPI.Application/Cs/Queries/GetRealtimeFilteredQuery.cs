using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetRealtimeFilteredQuery:IRequest<Result<List<FactRealtimeIndicator>>>
{
    public FilterDto Filter { get;  }

    public GetRealtimeFilteredQuery(FilterDto filter)
    {
        Filter = filter;
    }
}

public class GetRealtimeFilteredQueryHandler:IRequestHandler<GetRealtimeFilteredQuery,Result<List<FactRealtimeIndicator>>>
{
    private readonly ICsContext _context;

    public GetRealtimeFilteredQueryHandler(ICsContext context)
    {
        _context = context;
    }

    public async Task<Result<List<FactRealtimeIndicator>>> Handle(GetRealtimeFilteredQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<FactRealtimeIndicator> query = _context.FactRealtimeIndicators;

            // Indicator
            if (request.Filter.HasIndicator())
                query = query.Where(x => 
                    x.Indicator != null &&
                    x.Indicator.ToLower()==request.Filter.Indicator!.ToLower());
            
            // Time
            if (request.Filter.HasStartPeriod() && request.Filter.HasEndPeriod())
                query = query.Where(x => 
                    x.AssessmentPeriod>=request.Filter.StartPeriod! &&
                    x.AssessmentPeriod<=request.Filter.EndPeriod!);

            // Place
            if (request.Filter.HasCounty())
                query = query.Where(x =>request.Filter.County!.Contains(x.County));
           
            if (request.Filter.HasSubCounty())
                query = query.Where(x =>request.Filter.SubCounty!.Contains(x.SubCounty));
            
            if (request.Filter.HasFacilityName())
                query = query.Where(x =>request.Filter.FacilityName!.Contains(x.FacilityName));
            
            // Person
            if (request.Filter.HasSex())
                query = query.Where(x =>request.Filter.Sex!.Contains(x.Sex));
            
            if (request.Filter.HasAgeGroup())
                query = query.Where(x =>request.Filter.AgeGroup!.Contains(x.AgeGroup));

            // Partner
            if (request.Filter.HasAgency())
                query = query.Where(x =>request.Filter.Agency!.Contains(x.Agency));
            
            if (request.Filter.HasPartnerName())
                query = query.Where(x =>request.Filter.PartnerName!.Contains(x.PartnerName));
         
            var indicators = await query.ToListAsync(cancellationToken);
            
            return Result.Success(indicators);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<List<FactRealtimeIndicator>>(e.Message);
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