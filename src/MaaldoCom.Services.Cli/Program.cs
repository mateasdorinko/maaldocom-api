/*
using MaaldoCom.Services.Cli.Commands;
using MaaldoCom.Services.Cli.Infrastructure;
using MaaldoCom.Services.Cli.MaaldoApi;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Spectre.Console.Cli;

var services = new ServiceCollection();

services.AddRefitClient<IMaaldoApiClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://app-maaldocomapi-tst-cus.azurewebsites.net"));

var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.AddCommand<KnowledgeCommand>("knowledge")
        .WithDescription("List knowledge items from the API");
});

return await app.RunAsync(args);
*/
Console.ReadLine();