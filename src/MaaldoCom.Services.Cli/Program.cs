using MaaldoCom.Services.Cli.Commands;
using MaaldoCom.Services.Cli.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

const string asciiArt = """

                                                     __     __                                          __ _
                           ____ ___   ____ _ ____ _ / /____/ /____     _____ ____   ____ ___     _____ / /(_)
                          / __ `__ \ / __ `// __ `// // __  // __ \   / ___// __ \ / __ `__ \   / ___// // /
                         / / / / / // /_/ // /_/ // // /_/ // /_/ /_ / /__ / /_/ // / / / / /  / /__ / // /
                        /_/ /_/ /_/ \__,_/ \__,_//_/ \__,_/ \____/(_)\___/ \____//_/ /_/ /_/   \___//_//_/


                        """;

AnsiConsole.WriteLine(asciiArt);

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

    config.AddCommand<ListKnowledgeCommand>("list-knowledge")
        .WithDescription("List knowledge items from the API")
        .WithExample("list-knowledge", "dev")
        .WithExample("list-knowledge", "prod", "--random")
        .WithExample("list-knowledge", "test", "-r");
});

return await app.RunAsync(args);
