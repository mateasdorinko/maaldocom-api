using MaaldoCom.Services.Cli.Infrastructure;

namespace MaaldoCom.Services.Cli.Commands;

public sealed class ListKnowledgeSettings : CommandSettings
{
    [CommandArgument(0, "<environment>")]
    [Description("The target environment (local, dev, test, prod)")]
    public required string Environment { get; init; } = "dev";

    [CommandOption("-r|--random")]
    [Description("Get a random knowledge item instead of listing all")]
    public bool Random { get; init; } = false;

    public override ValidationResult Validate()
    {
        try
        {
            ApiEnvironmentExtensions.ParseEnvironment(Environment);
            return ValidationResult.Success();
        }
        catch (ArgumentException ex)
        {
            return ValidationResult.Error(ex.Message);
        }
    }
}

public sealed class ListKnowledgeCommand(IApiClientFactory clientFactory) : AsyncCommand<ListKnowledgeSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, ListKnowledgeSettings settings, CancellationToken cancellationToken)
    {
        var environment = ApiEnvironmentExtensions.ParseEnvironment(settings.Environment);
        var client = clientFactory.CreateClient(environment);

        AnsiConsole.MarkupLine($"[grey]Using environment:[/] [yellow]{environment}[/]");
        AnsiConsole.WriteLine();

        if (settings.Random)
        {
            await DisplayRandomKnowledge(client);
        }
        else
        {
            await DisplayAllKnowledge(client);
        }

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
            .AddColumn(new TableColumn("[bold]Title[/]").NoWrap())
            .AddColumn(new TableColumn("[bold]Quote[/]"));

        foreach (var item in knowledgeItems)
        {
            table.AddRow(
                Markup.Escape(item.Title),
                Markup.Escape(item.Quote)
            );
        }

        AnsiConsole.Write(table);
    }
}
