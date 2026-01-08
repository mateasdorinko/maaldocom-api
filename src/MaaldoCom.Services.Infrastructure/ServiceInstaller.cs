using MaaldoCom.Services.Application.Database;
using MaaldoCom.Services.Application.Cache;
using MaaldoCom.Services.Infrastructure.Database;
using MaaldoCom.Services.Infrastructure.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace MaaldoCom.Services.Infrastructure;

public static class ServiceInstaller
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MaaldoComDbContext>(options =>
        {
            options.UseSqlServer(configuration["maaldocom-db-connection-string-api-user"]);
        });
        services.AddScoped<IMaaldoComDbContext>(provider => provider.GetRequiredService<MaaldoComDbContext>());
        services.AddScoped<ICacheManager, CacheManager>();

        services.AddFusionCache()
            .WithDefaultEntryOptions(options => options.Duration = TimeSpan.FromMinutes(20))
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .AsHybridCache();
    }
}