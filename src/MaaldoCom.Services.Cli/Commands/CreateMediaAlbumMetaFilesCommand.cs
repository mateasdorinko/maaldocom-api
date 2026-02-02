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
        await mediaMetaDataCreator.CreateMediaMetaDataFilesAsync(settings.Path, cancellationToken);

        return 0;
    }
}