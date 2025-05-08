using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Cs;
using LiveDWAPI.Application.Stats.Dto;
using LiveDWAPI.Domain.Common;
using LiveDWAPI.Domain.Stats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Stats.Queries;

public class GetCurrentReportingQuery:IRequest<Result<CurrentReportingDto>>
{
    public DateTime? Period { get; }

    public GetCurrentReportingQuery(DateTime? period=null)
    {
        Period = period;
    }
}

public class GetCurrentReportingQueryHandler:IRequestHandler<GetCurrentReportingQuery,Result<CurrentReportingDto>>
{
    private ICsContext _csContext;

    public GetCurrentReportingQueryHandler(ICsContext csContext)
    {
        _csContext = csContext;
    }

    public async Task<Result<CurrentReportingDto>> Handle(GetCurrentReportingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var report = new CurrentReportingDto()
            {
                FacilityCount = 0,
                CountyCount = 0,
                PartnerCount = 0,
                Period=Utils.GetLastReportingMonth()
            };
            
            IQueryable<ReportingHistory> query  = _csContext.ReportingHistories.OrderByDescending(x=>x.Period);

            if (request.Period.HasValue)
            {
                query = query.Where(x => x.Period == request.Period.Value);
            }

            var history =await  query.FirstOrDefaultAsync(cancellationToken);
            
            if (history is not null)
            {
                report.FacilityCount = history.FacilityCount;
                report.CountyCount = history.CountyCount;
                report.PartnerCount = history.PartnerCount;
                report.Period = history.Period;
            }
        
            return Result.Success(report);
          
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<CurrentReportingDto>(e.Message);
        }
    }
}