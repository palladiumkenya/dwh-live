using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class DimIndicatorConfiguration : IEntityTypeConfiguration<DimIndicator>
{
    public void Configure(EntityTypeBuilder<DimIndicator> builder)
    {
        builder.ToView("vIndicatorGf").HasNoKey();
        builder.Property(e => e.Name).HasColumnName("Indicator");
    }
}