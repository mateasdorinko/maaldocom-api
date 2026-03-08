using System.Text;
using Azure.Storage.Blobs;
using MaaldoCom.Api.Application.Blobs;
using MaaldoCom.Api.Application.Database;
using MaaldoCom.Api.Application.Email;
using MaaldoCom.Api.Infrastructure.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Testcontainers.Azurite;
using Testcontainers.MsSql;
using Tests.Integration.TestHelpers;

namespace Tests.Integration;

[CollectionDefinition("Integration")]
public class IntegrationCollection : ICollectionFixture<App> { }

public class App : AppFixture<Program>
{
    private readonly MsSqlContainer _dbContainer
        = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();
    private readonly AzuriteContainer _blobContainer
        = new AzuriteBuilder("mcr.microsoft.com/azure-storage/azurite:latest").Build();

    protected override async ValueTask PreSetupAsync()
    {
        await Task.WhenAll(_dbContainer.StartAsync(), _blobContainer.StartAsync());
    }

    protected override async ValueTask SetupAsync()
    {
        await using var scope = Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();
        await db.Database.MigrateAsync();
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        a.UseSetting("auth0-domain", "test.example.com");
        a.UseSetting("auth0-audience", "test-audience");

        a.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<MaaldoComDbContext>>();
            services.RemoveAll<IDbContextFactory<MaaldoComDbContext>>();
            services.RemoveAll<IMaaldoComDbContext>();

            Action<DbContextOptionsBuilder> dbOpts = o => o.UseSqlServer(_dbContainer.GetConnectionString());
            services.AddDbContext<MaaldoComDbContext>(dbOpts);
            services.AddDbContextFactory<MaaldoComDbContext>(dbOpts, ServiceLifetime.Scoped);
            services.AddScoped<IMaaldoComDbContext>(p => p.GetRequiredService<MaaldoComDbContext>());

            services.RemoveAll<IBlobsProvider>();
            services.AddSingleton<IBlobsProvider>(
                _ => new AzureStorageBlobsProvider(new BlobServiceClient(_blobContainer.GetConnectionString())));

            services.RemoveAll<IEmailProvider>();
            services.AddSingleton<IEmailProvider, NoOpEmailProvider>();

            services.RemoveAll<IConfigureOptions<JwtBearerOptions>>();
            services.AddSingleton<IConfigureOptions<JwtBearerOptions>>(
                new ConfigureOptions<JwtBearerOptions>(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(TestConstants.JwtSigningKey)) // FastEndpoints.Testing constant
                    };
                }));
        });
    }

    protected override void ConfigureServices(IServiceCollection s) { }

    protected override async ValueTask TearDownAsync()
    {
        await _dbContainer.StopAsync();
        await _blobContainer.StopAsync();
    }
}
