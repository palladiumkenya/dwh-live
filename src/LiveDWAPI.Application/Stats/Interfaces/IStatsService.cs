using LiveDWAPI.Domain.Stats;

namespace LiveDWAPI.Application.Stats.Interfaces;

public interface IStatsService
{
    Task<List<SiteReporting>?> LoadCurrent();
}