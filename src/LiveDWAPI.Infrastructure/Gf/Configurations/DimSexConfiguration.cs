using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class DimSexConfiguration : IEntityTypeConfiguration<DimSex>
{
    public void Configure(EntityTypeBuilder<DimSex> builder)
    {
        builder.ToView("vSexGf").HasNoKey();
    }
}