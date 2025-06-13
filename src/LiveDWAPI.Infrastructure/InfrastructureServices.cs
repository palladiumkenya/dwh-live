using LiveDWAPI.Application.Cs;
using LiveDWAPI.Application.Gf;
using LiveDWAPI.Application.Stats.Interfaces;
using LiveDWAPI.Domain.Common;
using LiveDWAPI.Infrastructure.Cs;
using LiveDWAPI.Infrastructure.Gf;
using LiveDWAPI.Infrastructure.Stats;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, bool devMode = false, string overrideConnection = "")
    {
        var migrationsAssembly = typeof(CsContext).Assembly.FullName;
        var connectionString = string.IsNullOrWhiteSpace(overrideConnection)
            ? configuration.GetConnectionString("LiveConnection")
            : overrideConnection;

        services.AddDbContext<CsContext>(x => x
            .EnableSensitiveDataLogging(devMode)
            .UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(migrationsAssembly))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        
        services.AddDbContext<GfContext>(x => x
            .EnableSensitiveDataLogging(devMode)
            .UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(migrationsAssembly))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        
        services.Configure<ServicesApiOptions>(configuration.GetSection(ServicesApiOptions.ServicesApi));
        services.AddScoped<IStatsService,StatsService>();
        services.AddScoped<ICsContext>(provider => provider.GetRequiredService<CsContext>());
        services.AddScoped<IGfContext>(provider => provider.GetRequiredService<GfContext>());
        return services;
    }
}