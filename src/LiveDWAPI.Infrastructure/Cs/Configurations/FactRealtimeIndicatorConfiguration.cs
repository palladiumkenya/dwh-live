using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class FactRealtimeIndicatorConfiguration:IEntityTypeConfiguration<FactRealtimeIndicator>
{
    public void Configure(EntityTypeBuilder<FactRealtimeIndicator> builder)
    {
        builder.ToTable("vIndicatorData")
            .HasNoKey();
    }
}