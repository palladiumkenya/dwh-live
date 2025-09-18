using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetRealtimeFilteredExportQuery:IRequest<Result<IAsyncEnumerable<FactRealtimeIndicator>>>
{
    public FilterDto Filter { get;  }

    public GetRealtimeFilteredExportQuery(FilterDto filter)
    {
        Filter = filter;
    }
}

public class GetRealtimeFilteredExportQueryHandler:IRequestHandler<GetRealtimeFilteredExportQuery,Result<IAsyncEnumerable<FactRealtimeIndicator>>>
{
    private readonly ICsContext _context;

    public GetRealtimeFilteredExportQueryHandler(ICsContext context)
    {
        _context = context;
    }

    public async Task<Result<IAsyncEnumerable<FactRealtimeIndicator>>> Handle(GetRealtimeFilteredExportQuery request, CancellationToken cancellationToken)
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
            
            if (request.Filter.HasWard())
                query = query.Where(x =>request.Filter.Ward!.Contains(x.Ward));
            
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

            if (request.Filter.Limit > 0)
                query = query.Take(request.Filter.Limit);
            
            var indicators = query.AsAsyncEnumerable();
            
            return Result.Success(indicators);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<IAsyncEnumerable<FactRealtimeIndicator>>(e.Message);
        }
    }
}

// public class GetRealtimeFilteredExportQueryValidator: AbstractValidator<GetRealtimeFilteredExportQuery>
// {
//     public GetRealtimeFilteredExportQueryValidator()
//     {
//         RuleFor(x => x.Indicator).NotEmpty()
//             .WithMessage("Indicator is required");
//     }
// }