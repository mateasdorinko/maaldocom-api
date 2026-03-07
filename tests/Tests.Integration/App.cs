using Microsoft.AspNetCore.Hosting;
using Testcontainers.Azurite;
using Testcontainers.LowkeyVault;
using Testcontainers.MsSql;

namespace Tests.Integration;

public class App : AppFixture<Program>
{
    private readonly LowkeyVaultContainer _vaultContainer
        = new LowkeyVaultBuilder("ghcr.io/lowkeyvault/lowkeyvault:latest").Build();
    private readonly MsSqlContainer _dbContainer
        = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();
    private readonly AzuriteContainer _blobContainer
        = new AzuriteBuilder("mcr.microsoft.com/azure-storage/azurite:latest").Build();

    protected override async ValueTask PreSetupAsync()
    {

    }

    protected override async ValueTask SetupAsync()
    {
        await _vaultContainer.StartAsync();
        await _dbContainer.StartAsync();
        await _blobContainer.StartAsync();
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        // do host builder configuration here
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        // do test service registration here
    }

    protected override async ValueTask TearDownAsync()
    {
        await _vaultContainer.StopAsync();
        await _dbContainer.StopAsync();
        await _blobContainer.StopAsync();
    }
}
