using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class DimAgeGroupConfiguration : IEntityTypeConfiguration<DimAgeGroup>
{
    public void Configure(EntityTypeBuilder<DimAgeGroup> builder)
    {
        builder.ToView("vAgeGroupGf").HasNoKey();
    }
}