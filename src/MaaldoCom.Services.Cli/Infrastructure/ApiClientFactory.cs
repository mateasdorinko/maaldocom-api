using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Refit;

namespace MaaldoCom.Services.Cli.Infrastructure;

public sealed class ApiClientFactory(IConfiguration configuration) : IApiClientFactory
{
    private readonly RefitSettings _refitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        })
    };

    public IMaaldoApiClient CreateClient(ApiEnvironment environment)
    {
        var configKey = $"apiEnvironments:{environment.ToConfigKey()}:maaldoApiBaseUrl";
        var baseUrl = configuration[configKey];

        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new InvalidOperationException($"Base URL not configured for environment: {environment}");
        }

        var httpClient = CreateHttpClient(environment, baseUrl);
        return RestService.For<IMaaldoApiClient>(httpClient, _refitSettings);
    }

    private static HttpClient CreateHttpClient(ApiEnvironment environment, string baseUrl)
    {
        if (environment != ApiEnvironment.Local) return new HttpClient { BaseAddress = new Uri(baseUrl) };

#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
#pragma warning restore S4830

        return new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
    }
}