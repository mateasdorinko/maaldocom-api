using MaaldoCom.Api.Infrastructure.Blobs;
using MaaldoCom.Api.Infrastructure.Cache;
using MaaldoCom.Api.Infrastructure.Database;
using MaaldoCom.Api.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace MaaldoCom.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructureServices(IConfiguration configuration)
        {
            Action<DbContextOptionsBuilder> dbOptions = options =>
                options.UseSqlServer(configuration["maaldocom-db-connection-string-api-user"], providerOptions => providerOptions.EnableRetryOnFailure());

            services.AddDbContext<MaaldoComDbContext>(dbOptions);
            services.AddDbContextFactory<MaaldoComDbContext>(dbOptions, ServiceLifetime.Scoped);
            services.AddScoped<IMaaldoComDbContext>(provider => provider.GetRequiredService<MaaldoComDbContext>());
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddSingleton<IBlobsProvider, AzureStorageBlobsProvider>(_
                => new AzureStorageBlobsProvider(configuration["azure-storage-account-connection-string"]!));
            services.AddSingleton<IEmailProvider, MailGunEmailProvider>(_
                => new MailGunEmailProvider(configuration["mailgun-api-key"]!,
                    configuration["mailgun-domain"]!,
                    configuration["mailgun-base-url"]!,
                    configuration["mailgun-default-from-email"]!,
                    configuration["mailgun-default-to-email"]!));

            services.AddFusionCache()
                .WithDefaultEntryOptions(options => options.Duration = TimeSpan.FromMinutes(20))
                .WithSerializer(new FusionCacheSystemTextJsonSerializer())
                .AsHybridCache();

            return services;
        }
    }
}
