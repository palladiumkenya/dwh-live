using System.Reflection;
using LiveDWAPI.Application.Cs;
using LiveDWAPI.Domain.Cs;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Infrastructure.Cs;

public class CsContext : DbContext, ICsContext
{
    
    public DbSet<DimIndicator> DimIndicators => Set<DimIndicator>();
    public DbSet<DimRegion> DimRegions => Set<DimRegion>();
    public DbSet<DimAgency> DimAgencies => Set<DimAgency>();
    public DbSet<DimAgeGroup> DimAgeGroups => Set<DimAgeGroup>();
    public DbSet<DimSex> DimSex => Set<DimSex>();
    public DbSet<FactRealtimeIndicator> FactRealtimeIndicators => Set<FactRealtimeIndicator>();
    
    public CsContext(DbContextOptions<CsContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void Initialize()
    {
        try
        {
            Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}