namespace MaaldoCom.Services.Application.MediaMetaData;

public interface IMediaMetaDataCreator
{
    Task<Stream> CreateThumbnailMediaAsync();
    Task<Stream> CreateViewerMediaAsync();
}