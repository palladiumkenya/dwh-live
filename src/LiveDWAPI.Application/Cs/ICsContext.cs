using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;

namespace LiveDWAPI.Application.Cs;

public interface ICsContext
{
    DbSet<DimIndicator> DimIndicators { get; }
    DbSet<DimRegion> DimRegions{ get; }
    DbSet<DimAgency> DimAgencies{ get; }
    DbSet<DimAgeGroup> DimAgeGroups { get; }
    DbSet<DimSex> DimSex { get; }
    DbSet<FactRealtimeIndicator> FactRealtimeIndicators { get; }
}