namespace MaaldoCom.Services.Cli.Commands;

public static class CacheRefreshCommandConfigurator
{
    public static void AddCacheRefreshCommand(this IConfigurator configurator)
    {
        configurator.AddCommand<CacheRefreshCommand>("cache-refresh")
            .WithDescription("Refreshes the cached lists from the API")
            .WithExample("cache-refresh", "dev");
    }
}

public sealed class CacheRefreshCommandSettings : BaseApiCommandSettings { }

public class CacheRefreshCommand(IApiClientFactory clientFactory) : AsyncCommand<CacheRefreshCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, CacheRefreshCommandSettings settings, CancellationToken cancellationToken)
    {
        var environment = ApiEnvironmentExtensions.ParseEnvironment(settings.Environment);
        var client = clientFactory.CreateClient(environment);

        AnsiConsole.MarkupLine($"[grey]Using environment:[/] [yellow]{environment}[/]");

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Refreshing cache...", async _ =>
            {
                await client.RefreshCache();
            });

        AnsiConsole.MarkupLine($"[grey]Cache refreshed.[/]");

        return 0;
    }
}