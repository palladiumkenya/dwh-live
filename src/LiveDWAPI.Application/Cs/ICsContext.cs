using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;

namespace LiveDWAPI.Application.Cs;

public interface ICsContext
{
    DbSet<DimIndicator> DimIndicators { get; }
    DbSet<FactRealtimeIndicator> FactRealtimeIndicators { get; }
}