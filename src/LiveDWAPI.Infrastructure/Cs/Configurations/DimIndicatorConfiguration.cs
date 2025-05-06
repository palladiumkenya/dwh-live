using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class DimIndicatorConfiguration : IEntityTypeConfiguration<DimIndicator>
{
    public void Configure(EntityTypeBuilder<DimIndicator> builder)
    {
        builder.ToView("vIndicator").HasNoKey();
        builder.Property(e => e.Name).HasColumnName("Indicator");
    }
}

public class DimAgeGroupConfiguration : IEntityTypeConfiguration<DimAgeGroup>
{
    public void Configure(EntityTypeBuilder<DimAgeGroup> builder)
    {
        builder.ToView("vAgeGroup").HasNoKey();
    }
}

public class DimAgencyConfiguration : IEntityTypeConfiguration<DimAgency>
    {
        public void Configure(EntityTypeBuilder<DimAgency> builder)
        {
            builder.ToView("vAgencyPartner").HasNoKey();
        }
    }

    public class DimRegionConfiguration : IEntityTypeConfiguration<DimRegion>
    {
        public void Configure(EntityTypeBuilder<DimRegion> builder)
        {
            builder.ToView("vRegionFacility").HasNoKey();
        }
    }

    public class DimSexConfiguration : IEntityTypeConfiguration<DimSex>
    {
        public void Configure(EntityTypeBuilder<DimSex> builder)
        {
            builder.ToView("vSex").HasNoKey();
        }
    }
    