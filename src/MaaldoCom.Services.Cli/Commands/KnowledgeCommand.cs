namespace MaaldoCom.Services.Cli.Commands;

public static class KnowledgeCommandConfigurator
{
    public static void AddKnowledgeCommand(this IConfigurator configurator)
    {
        configurator.AddCommand<KnowledgeCommand>("knowledge")
            .WithDescription("Lists knowledge from the API")
            .WithExample("knowledge", "dev")
            .WithExample("knowledge", "--random", "prod")
            .WithExample("knowledge", "-r", "test");
    }
}

public sealed class KnowledgeCommandSettings : BaseApiCommandSettings
{
    [CommandOption("-r|--random")]
    [Description("Gets a random knowledge item")]
    public bool Random { get; init; } = false;
}

public sealed class KnowledgeCommand(IApiClientFactory clientFactory) : AsyncCommand<KnowledgeCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, KnowledgeCommandSettings settings, CancellationToken cancellationToken)
    {
        var environment = ApiEnvironmentExtensions.ParseEnvironment(settings.Environment);
        var client = clientFactory.CreateClient(environment);

        AnsiConsole.MarkupLine($"[grey]Using environment:[/] [yellow]{environment}[/]");

        if (settings.Random) { await DisplayRandomKnowledge(client); }
        else { await DisplayAllKnowledge(client); }

        return 0;
    }

    private static async Task DisplayRandomKnowledge(IMaaldoApiClient client)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Fetching random knowledge...", async _ =>
            {
                var knowledge = await client.GetRandomKnowledge();
                DisplayKnowledgeTable([knowledge]);
            });
    }

    private static async Task DisplayAllKnowledge(IMaaldoApiClient client)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Fetching knowledge...", async _ =>
            {
                var knowledgeList = await client.ListKnowledge();
                DisplayKnowledgeTable(knowledgeList);
            });
    }

    private static void DisplayKnowledgeTable(IEnumerable<GetKnowledgeResponse> knowledgeItems)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn(new TableColumn("[bold]Id[/]").NoWrap())
            .AddColumn(new TableColumn("[bold]Title[/]").NoWrap())
            .AddColumn(new TableColumn("[bold]Quote[/]"));

        foreach (var item in knowledgeItems)
        {
            table.AddRow(
                Markup.Escape(item.Id.ToString()),
                Markup.Escape(item.Title),
                Markup.Escape(item.Quote)
            );
        }

        AnsiConsole.Write(table);
    }
}
