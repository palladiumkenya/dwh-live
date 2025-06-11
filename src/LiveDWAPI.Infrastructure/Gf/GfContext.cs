using System.Reflection;
using LiveDWAPI.Application.Gf;
using LiveDWAPI.Domain.Gf;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Infrastructure.Gf;

public class GfContext : DbContext, IGfContext
{
    public DbSet<DimIndicator> DimIndicators => Set<DimIndicator>();
    public DbSet<DimRegion> DimRegions => Set<DimRegion>();
    public DbSet<DimAgency> DimAgencies => Set<DimAgency>();
    public DbSet<DimAgeGroup> DimAgeGroups => Set<DimAgeGroup>();
    public DbSet<DimSex> DimSex => Set<DimSex>();
    public DbSet<FactAggregateIndicator> FactAggregateIndicators => Set<FactAggregateIndicator>();


    public GfContext(DbContextOptions<GfContext> options) : base(options)
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
            Database.MigrateAsync().Wait();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    
    public Task Commit(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
}