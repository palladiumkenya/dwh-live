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