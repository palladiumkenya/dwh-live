using LiveDWAPI.Application.Cs;
using LiveDWAPI.Infrastructure.Cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, bool devMode = false, string overrideConnection = "")
    {
        var connectionString = string.IsNullOrWhiteSpace(overrideConnection)
            ? configuration.GetConnectionString("LiveConnection")
            : overrideConnection;

        services.AddDbContext<CsContext>(x => x
            .EnableSensitiveDataLogging(devMode)
            .UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        

        services.AddScoped<ICsContext>(provider => provider.GetRequiredService<CsContext>());
        return services;
    }
}