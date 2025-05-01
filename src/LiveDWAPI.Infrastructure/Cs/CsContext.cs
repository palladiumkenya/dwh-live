using System.Reflection;
using LiveDWAPI.Application.Cs;
using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;

namespace LiveDWAPI.Infrastructure.Cs;

public class CsContext : DbContext, ICsContext
{
    public DbSet<DimIndicator> DimIndicators => Set<DimIndicator>();
    public DbSet<FactRealtimeIndicator> FactRealtimeIndicators => Set<FactRealtimeIndicator>();

    public CsContext(DbContextOptions<CsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
    }
}