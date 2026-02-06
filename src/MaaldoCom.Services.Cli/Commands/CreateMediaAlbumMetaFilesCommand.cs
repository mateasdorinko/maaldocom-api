using System.Text.Json;
using MaaldoCom.Services.Application.MediaMetaData;
using MaaldoCom.Services.Domain.MediaAlbums;

namespace MaaldoCom.Services.Cli.Commands;

public static class CreateMediaAlbumMetaFilesCommandConfigurator
{
    private const string CommandName = "create-mediaalbum-metafiles";

    public static void AddCreateMediaAlbumMetaFilesCommand(this IConfigurator configurator)
    {
        configurator.AddCommand<CreateMediaAlbumMetaFilesCommand>(CommandName)
            .WithDescription("Creates associated media album metadata files")
            .WithExample(CommandName, @"C:\\Users\\maaldo\\Desktop\\test-media-album");
    }
}

public sealed class CreateMediaAlbumMetaFilesCommandSettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    [Description("The path to the media album directory")]
    public required string Path { get; init; } = string.Empty;
}

public sealed class CreateMediaAlbumMetaFilesCommand(IMediaMetaDataCreator mediaMetaDataCreator) : AsyncCommand<CreateMediaAlbumMetaFilesCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, CreateMediaAlbumMetaFilesCommandSettings settings, CancellationToken cancellationToken)
    {
        var mediaAlbumFolder = new DirectoryInfo(settings.Path);
        var now = DateTime.Now;

        var postRequest = new PostMediaAlbumRequest
        {
            Name = MediaAlbumHelper.GetProperNameFromFolder(mediaAlbumFolder.Name),
            UrlFriendlyName = mediaAlbumFolder.Name,
            Description = string.Empty,
            Created = new DateTimeOffset(now.Year, now.Month, now.Day, 12, 0, 0, TimeSpan.Zero),
            Tags = [],
            Media = mediaAlbumFolder.GetFiles().Select(f => new PostMediaRequest
            {
                FileName = f.Name,
                Description = string.Empty,
                FileExtension = f.Extension,
                SizeInBytes = f.Length,
                Tags = []
            }).ToList()
        };

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Abra cadabra...", async _ =>
            {
                await mediaMetaDataCreator.CreateMediaMetaDataFilesAsync(settings.Path, AnsiConsole.MarkupLine, cancellationToken);
            });

        var json = JsonSerializer.Serialize(postRequest, options: new JsonSerializerOptions { WriteIndented = true });
        const string requestFileName = $"post-mediaalbum-request.json";

        await File.WriteAllTextAsync($"{settings.Path}/{requestFileName}", json, cancellationToken);

        AnsiConsole.MarkupLine(string.Empty);
        AnsiConsole.MarkupLine($"[grey]Media album metadata files created successfully.[/]");
        AnsiConsole.MarkupLine(string.Empty);
        AnsiConsole.MarkupLine($"[grey]PostMediaAlbumRequest requestion file created successfully:[/] [yellow]{requestFileName}[/]");
        AnsiConsole.MarkupLine(string.Empty);

        return 0;
    }
}
