using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;

namespace LiveDWAPI.Application.Gf;

public interface IGfContext
{
    DbSet<DimIndicator> DimIndicators { get; }
    DbSet<DimRegion> DimRegions{ get; }
    DbSet<DimAgency> DimAgencies{ get; }
    DbSet<DimAgeGroup> DimAgeGroups { get; }
    DbSet<DimSex> DimSex { get; }
    DbSet<FactAggregateIndicator> FactAggregateIndicators { get; }
    Task Commit(CancellationToken cancellationToken);
}