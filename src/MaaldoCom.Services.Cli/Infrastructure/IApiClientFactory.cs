namespace MaaldoCom.Services.Cli.Infrastructure;

public interface IApiClientFactory
{
    IMaaldoApiClient CreateClient(ApiEnvironment environment);
}
