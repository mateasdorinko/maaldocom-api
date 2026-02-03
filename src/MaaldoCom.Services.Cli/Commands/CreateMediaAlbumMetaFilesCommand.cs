using System.Text.Json;
using MaaldoCom.Services.Application.MediaMetaData;

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
            Name = mediaAlbumFolder.Name,
            UrlFriendlyName = mediaAlbumFolder.Name,
            Description = string.Empty,
            Created = new DateTimeOffset(now.Year, now.Month, now.Day, 12, 0, 0, TimeSpan.Zero),
            Tags = [],
            Media = mediaAlbumFolder.GetFiles().Select(f => new PostMediumRequest
            {
                FileName = f.Name,
                Description = string.Empty,
                FileExtension = f.Extension,
                SizeInBytes = f.Length,
                Tags = []
            }).ToList()
        };

        await mediaMetaDataCreator.CreateMediaMetaDataFilesAsync(settings.Path, cancellationToken);

        var json = JsonSerializer.Serialize(postRequest, options: new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync($"{settings.Path}/request.json", json, cancellationToken);
        Console.WriteLine("PostMediaAlbumRequest file (request.json) created successfully.");

        return 0;
    }
}
