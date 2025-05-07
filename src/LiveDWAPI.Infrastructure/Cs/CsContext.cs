using System.Reflection;
using LiveDWAPI.Application.Cs;
using LiveDWAPI.Domain.Cs;
using LiveDWAPI.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Infrastructure.Cs;

public class CsContext : DbContext, ICsContext
{
    public CsContext(DbContextOptions<CsContext> options) : base(options)
    {
    }

    public DbSet<AppSystem> AppSystems => Set<AppSystem>();
    public DbSet<DimIndicator> DimIndicators => Set<DimIndicator>();
    public DbSet<DimRegion> DimRegions => Set<DimRegion>();
    public DbSet<DimAgency> DimAgencies => Set<DimAgency>();
    public DbSet<DimAgeGroup> DimAgeGroups => Set<DimAgeGroup>();
    public DbSet<DimSex> DimSex => Set<DimSex>();
    public DbSet<FactRealtimeIndicator> FactRealtimeIndicators => Set<FactRealtimeIndicator>();


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