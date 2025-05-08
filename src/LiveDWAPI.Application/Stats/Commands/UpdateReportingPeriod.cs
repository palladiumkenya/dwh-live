using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Cs;
using LiveDWAPI.Application.Stats.Interfaces;
using LiveDWAPI.Domain.Common;
using LiveDWAPI.Domain.Stats;
using MediatR;
using Serilog;

namespace LiveDWAPI.Application.Stats.Commands;

public class UpdateReportingPeriod:IRequest<Result>
{
    public bool Force { get; }

    public UpdateReportingPeriod(bool force)
    {
        Force = force;
    }
}

public class UpdateReportingPeriodHandler:IRequestHandler<UpdateReportingPeriod,Result>
{
    private readonly IStatsService _service;
    private readonly ICsContext _csContext;

    public UpdateReportingPeriodHandler(IStatsService service, ICsContext csContext)
    {
        _service = service;
        _csContext = csContext;
    }

    public async Task<Result> Handle(UpdateReportingPeriod request, CancellationToken cancellationToken)
    {
        try
        {
            var current = await _csContext.ReportingHistories.FindAsync(Utils.GetLastReportingMonth());
            
            if (request.Force)
            {
                var liveReport = await _service.LoadCurrent();
                if (liveReport is not null)
                {
                    var newReport = ReportingHistory.Generate(liveReport);
                   
                    if (current is not null)
                    {
                        current.UpdateStats(newReport);
                        _csContext.ReportingHistories.Update(current);
                        await _csContext.Commit(cancellationToken);
                        return Result.Success();
                    }

                    await _csContext.ReportingHistories.AddAsync(newReport, cancellationToken);
                    await _csContext.Commit(cancellationToken);
                    return Result.Success();
                }
            }

            if (current is null)
            {
                var liveReport = await _service.LoadCurrent();
                if (liveReport is not null)
                {
                    var newReport = ReportingHistory.Generate(liveReport);  
                    await _csContext.ReportingHistories.AddAsync(newReport, cancellationToken);
                    await _csContext.Commit(cancellationToken);
                }
            }
            return Result.Success();
        }
        catch (Exception e)
        {
           Log.Error(e,"Update Error!");
           return Result.Failure<UpdateReportingPeriod>(e.Message);
        }
    }
}