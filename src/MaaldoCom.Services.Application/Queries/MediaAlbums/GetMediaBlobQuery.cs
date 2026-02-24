using MaaldoCom.Services.Application.Blobs;
using MaaldoCom.Services.Domain.MediaAlbums;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaBlobQuery(Guid mediaAlbumId, Guid mediaId, string mediaType) : ICommand<Result<MediaDto>>
{
    public Guid MediaAlbumId { get; } = mediaAlbumId;
    public Guid MediaId { get; } = mediaId;
    public string MediaType { get; } = mediaType;
}

public class GetMediaBlobQueryHandler(ICacheManager cacheManager, IBlobsProvider blobsProvider)
    : ICommandHandler<GetMediaBlobQuery, Result<MediaDto>>
{
    public async Task<Result<MediaDto>> ExecuteAsync(GetMediaBlobQuery query, CancellationToken ct)
    {
        const string containerName = "media-albums";
        var notFoundResult = Result.Fail<MediaDto>(new BlobNotFoundError(containerName, $"MediaAlbum:{query.MediaAlbumId}/Media:{query.MediaId}"));

        var mediaAlbum = await cacheManager.GetMediaAlbumDetailAsync(query.MediaAlbumId, ct);
        var media = mediaAlbum?.Media.FirstOrDefault(m => m.Id == query.MediaId);

        if (media == null) { return notFoundResult; }

        // account for all mutations of blob names based on media type (original/viewer/thumb) and file type (pic/vid)
        string blobName;
        switch (query.MediaType)
        {
            case "original":
                blobName = $"{MediaAlbumHelper.GetOriginalMetaFilePath(mediaAlbum?.UrlFriendlyName!, media.FileName!)}";
                break;
            case "viewer":
                blobName = $"{MediaAlbumHelper.GetViewerMetaFilePath(mediaAlbum?.UrlFriendlyName!, media.FileName!)}";
                break;
            case "thumb":
                blobName = $"{MediaAlbumHelper.GetThumbnailMetaFilePath(mediaAlbum?.UrlFriendlyName!, media.FileName!)}";
                break;
            default:
                return notFoundResult;
        }

        var dto = await blobsProvider.GetBlobAsync(containerName, blobName, ct);

        return dto != null ?
            Result.Ok(dto)! :
            Result.Fail<MediaDto>(new BlobNotFoundError(containerName, blobName));
    }
}
