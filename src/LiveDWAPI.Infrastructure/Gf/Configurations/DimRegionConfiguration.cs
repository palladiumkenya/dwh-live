using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class DimRegionConfiguration : IEntityTypeConfiguration<DimRegion>
{
    public void Configure(EntityTypeBuilder<DimRegion> builder)
    {
        builder.ToView("vRegionFacilityGf").HasNoKey();
    }
}