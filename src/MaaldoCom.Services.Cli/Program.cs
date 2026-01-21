using MaaldoCom.Services.Cli.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var figlet = new FigletText("maaldo.com cli")
    {
        Color = Color.Blue
    };

AnsiConsole.Write(figlet);

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);
services.AddSingleton<IApiClientFactory, ApiClientFactory>();

var registrar = new TypeRegistrar(services);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("maaldocom-cli");
    config.AddKnowledgeCommand();
    config.AddTagsCommand();
    config.AddCacheRefreshCommand();
});

return await app.RunAsync(args);
