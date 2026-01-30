namespace MaaldoCom.Services.Infrastructure.MediaMetaData;

public class FFmpegMediaMetaDataCreator : IMediaMetaDataCreator
{
    public async Task<Stream> CreateThumbnailMediaAsync()
    {
        await Task.CompletedTask;
        return Stream.Null;
    }

    public async Task<Stream> CreateViewerMediaAsync()
    {
        return await CreateThumbnailMediaAsync();
    }
}