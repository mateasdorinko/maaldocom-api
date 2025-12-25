using MaaldoCom.Services.Application.Interfaces;
using MaaldoCom.Services.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace MaaldoCom.Services.Infrastructure;

public class ServiceInstaller
{
    public static void InstallInfrastructureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MaaldoComDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("maaldocom-db-connection-string-api-user"));
        });
        services.AddScoped<IMaaldoComDbContext>(provider => provider.GetRequiredService<MaaldoComDbContext>());

        services.AddFusionCache()
            .WithDefaultEntryOptions(options => options.Duration = TimeSpan.FromMinutes(5))
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .WithMemoryCache(TimeSpan.FromMinutes(5))
            .AsHybridCache();
    }
}