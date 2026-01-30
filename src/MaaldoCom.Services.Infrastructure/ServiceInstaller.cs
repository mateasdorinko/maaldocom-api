using MaaldoCom.Services.Infrastructure.Database;
using MaaldoCom.Services.Infrastructure.Cache;
using MaaldoCom.Services.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace MaaldoCom.Services.Infrastructure;

public static class ServiceInstaller
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MaaldoComDbContext>(options =>
        {
            options.UseSqlServer(configuration["maaldocom-db-connection-string-api-user"], providerOptions => providerOptions.EnableRetryOnFailure());
        });
        services.AddScoped<IMaaldoComDbContext>(provider => provider.GetRequiredService<MaaldoComDbContext>());
        services.AddScoped<ICacheManager, CacheManager>();
        services.AddScoped<IEmailProvider, SendGridEmailProvider>(_
            => new SendGridEmailProvider(configuration["sendgrid-api-key"]!,
                configuration["sendgrid-default-from-email"]!,
                configuration["sendgrid-default-to-email"]!));

        services.AddFusionCache()
            .WithDefaultEntryOptions(options => options.Duration = TimeSpan.FromMinutes(20))
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .AsHybridCache();

        return services;
    }
}