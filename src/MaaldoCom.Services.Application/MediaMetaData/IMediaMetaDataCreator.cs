namespace MaaldoCom.Services.Application.MediaMetaData;

public interface IMediaMetaDataCreator
{
    Task CreateMediaMetaDataFilesAsync(string mediaAlbumFolderPath, CancellationToken cancellationToken);
}