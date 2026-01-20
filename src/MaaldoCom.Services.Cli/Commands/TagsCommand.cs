namespace MaaldoCom.Services.Cli.Commands;

public static class TagsCommandConfigurator
{
    public static void AddTagsCommand(this IConfigurator configurator)
    {
        configurator.AddCommand<TagsCommand>("tags")
                .WithDescription("Lists tags from the API")
                .WithExample("tags", "local")
                .WithExample("tags", "--name", "austin", "prod")
                .WithExample("tags", "-n", "hunter", "test");
    }
}

public sealed class TagsCommandSettings : BaseApiCommandSettings
{
    [CommandOption("-n|--name")]
    [Description("Gets the tags associated with the name")]
    public string Name { get; init; } = string.Empty;
}

public sealed class TagsCommand(IApiClientFactory clientFactory) : AsyncCommand<TagsCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, TagsCommandSettings settings, CancellationToken cancellationToken)
    {
        var environment = ApiEnvironmentExtensions.ParseEnvironment(settings.Environment);
        var client = clientFactory.CreateClient(environment);

        AnsiConsole.MarkupLine($"[grey]Using environment:[/] [yellow]{environment}[/]");

        if (!string.IsNullOrWhiteSpace(settings.Name)) { await DisplayTagsByName(client, settings.Name); }
        else { await DisplayAllTags(client); }

        return 0;
    }

    private static async Task DisplayTagsByName(IMaaldoApiClient client, string name)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync($"Fetching tags for name '{name}'...", async _ =>
            {
                var tagDetail = await client.GetTagByName(name);
                var table = GetTagsTable();

                table.AddRow(Markup.Escape(tagDetail.Id.ToString()), Markup.Escape(tagDetail.Name));

                var mediaAlbumTagsTable = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn(new TableColumn("[bold]MediaAlbumName[/]").NoWrap())
                    .AddColumn(new TableColumn("[bold]Href[/]").NoWrap());
                var mediaTagsTable = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn(new TableColumn("[bold]MediaName[/]").NoWrap())
                    .AddColumn(new TableColumn("[bold]MediaAlbumName[/]").NoWrap())
                    .AddColumn(new TableColumn("[bold]Href[/]").NoWrap());

                table.AddRow(mediaAlbumTagsTable);
                table.AddRow(mediaTagsTable);

                foreach (var item in tagDetail.MediaAlbums)
                {
                    mediaAlbumTagsTable.AddRow(
                        Markup.Escape(item.Name),
                        Markup.Escape(item.Href)
                    );
                }

                foreach (var item in tagDetail.Media)
                {
                    mediaTagsTable.AddRow(
                        Markup.Escape(item.Name),
                        Markup.Escape(item.MediaAlbumName),
                        Markup.Escape(item.Href)
                    );
                }

                AnsiConsole.Write(table);
            });
    }

    private static async Task DisplayAllTags(IMaaldoApiClient client)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Fetching all tags...", async _ =>
            {
                var tags = await client.ListTags();
                var table = GetTagsTable();

                foreach (var item in tags)
                {
                    table.AddRow(
                        Markup.Escape(item.Id.ToString()),
                        Markup.Escape(item.Name),
                        Markup.Escape(item.AltHref)
                    );
                }

                AnsiConsole.Write(table);
            });
    }

    private static Table GetTagsTable()
    {
        return new Table()
            .Border(TableBorder.Rounded)
            .AddColumn(new TableColumn("[bold]Id[/]").NoWrap())
            .AddColumn(new TableColumn("[bold]Name[/]").NoWrap())
            .AddColumn(new TableColumn("[bold]AltHref[/]").NoWrap());
    }
}