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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Testcontainers.Azurite;
using Testcontainers.MsSql;

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
            services.AddSingleton<IEmailProvider, MockEmailProvider>();

            services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.ConfigurationManager = null;
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(TestConstants.JwtSigningKey))
                };
            });
        });
    }

    protected override void ConfigureServices(IServiceCollection s) { }

    protected override async ValueTask TearDownAsync()
    {
        await _dbContainer.StopAsync();
        await _blobContainer.StopAsync();
    }

    public HttpClient GetUnauthorizedClient() => CreateClient();

    public HttpClient GetAuthorizedClient(IEnumerable<string> permissions)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TestConstants.JwtSigningKey));
        var claims = permissions.Select(p => new Claim("permissions", p));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(token));
        return client;
    }
}
