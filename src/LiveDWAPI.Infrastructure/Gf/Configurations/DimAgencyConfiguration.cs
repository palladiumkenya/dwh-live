using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Gf.Configurations;

public class DimAgencyConfiguration : IEntityTypeConfiguration<DimAgency>
{
    public void Configure(EntityTypeBuilder<DimAgency> builder)
    {
        builder.ToView("vAgencyPartnerGf").HasNoKey();
        builder.Property(e => e.Agency).HasColumnName("AgencyName");
    }
}