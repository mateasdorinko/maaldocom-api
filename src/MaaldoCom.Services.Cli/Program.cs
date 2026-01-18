using MaaldoCom.Services.Cli;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
var services = new ServiceCollection();
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true
};

services.AddRefitClient<IMaaldoApiClient>(new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(options)
    })
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["maaldoApiBaseUrl"]!));

var client = RestService.For<IMaaldoApiClient>(config["maaldoApiBaseUrl"]!);

for (var i = 0; i < 100; i++)
{
    var response = await client.GetRandomKnowledge();
    Console.WriteLine($"{response.Title} - {response.Quote}");
}
