using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class DimRegionConfiguration : IEntityTypeConfiguration<DimRegion>
{
    public void Configure(EntityTypeBuilder<DimRegion> builder)
    {
        builder.ToView("vRegionFacility").HasNoKey();
    }
}