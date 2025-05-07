using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class DimAgeGroupConfiguration : IEntityTypeConfiguration<DimAgeGroup>
{
    public void Configure(EntityTypeBuilder<DimAgeGroup> builder)
    {
        builder.ToView("vAgeGroup").HasNoKey();
    }
}