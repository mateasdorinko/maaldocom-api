using MaaldoCom.Services.Cli;
using MaaldoCom.Services.Cli.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Text.Json;
using Spectre.Console.Cli;

var services = new ServiceCollection();
var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true
};

services.AddRefitClient<IMaaldoApiClient>(new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(options)
    })
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.maaldo.com"));

var client = RestService.For<IMaaldoApiClient>("https://app-maaldocomapi-tst-cus.azurewebsites.net");

var response = await client.GetRandomKnowledge();
Console.WriteLine($"Random Knowledge: {response.Title} - {response.Quote}");

/*
app.Configure(config =>
{
    config.AddCommand<KnowledgeCommand>("knowledge")
        .WithDescription("List knowledge items from the API");
});

return await app.RunAsync(args);
*/