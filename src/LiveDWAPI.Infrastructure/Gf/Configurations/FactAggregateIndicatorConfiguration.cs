using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class FactAggregateIndicatorConfiguration:IEntityTypeConfiguration<FactAggregateIndicator>
{
    public void Configure(EntityTypeBuilder<FactAggregateIndicator> builder)
    {
        builder.ToView("CSAggregateBMGFIndicators").HasNoKey();
        builder.Property(e => e.Agency).HasColumnName("AgencyName");
    }
}