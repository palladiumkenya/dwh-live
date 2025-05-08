using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveDWAPI.Infrastructure.Cs.Configurations;

public class DimAgencyConfiguration : IEntityTypeConfiguration<DimAgency>
{
    public void Configure(EntityTypeBuilder<DimAgency> builder)
    {
        builder.ToView("vAgencyPartner").HasNoKey();
    }
}