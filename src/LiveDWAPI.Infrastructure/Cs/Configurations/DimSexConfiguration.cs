using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class DimSexConfiguration : IEntityTypeConfiguration<DimSex>
{
    public void Configure(EntityTypeBuilder<DimSex> builder)
    {
        builder.ToView("vSex").HasNoKey();
    }
}