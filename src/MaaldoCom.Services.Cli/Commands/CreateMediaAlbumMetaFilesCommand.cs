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

        var postRequest = new PostMediaAlbumRequest
        {
            Name = mediaAlbumFolder.Name,
            UrlFriendlyName = mediaAlbumFolder.Name,
            Description = "TEST_DESCRIPTION",
            Created = DateTime.Now,
            Media = mediaAlbumFolder.GetFiles().Select(f => new PostMediumRequest
            {
                FileName = f.Name,
                Description = "TEST_DESCRIPTION",
                FileExtension = f.Extension,
                SizeInBytes = f.Length,
                Tags = ["TAG1", "TAG2"]
            }).ToList(),
            Tags = ["TAG1", "TAG2"]
        };

        await mediaMetaDataCreator.CreateMediaMetaDataFilesAsync(settings.Path, cancellationToken);

        var json = JsonSerializer.Serialize(postRequest, options: new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync($"{settings.Path}/request.json", json, cancellationToken);
        Console.WriteLine("PostMediaAlbumRequest file (request.json) created successfully.");

        return 0;
    }
}