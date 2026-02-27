using MaaldoCom.Api.Application.Blobs;
using MaaldoCom.Api.Domain.MediaAlbums;

namespace MaaldoCom.Api.Application.Queries.MediaAlbums;

public sealed record GetMediaBlobQuery(Guid MediaAlbumId, Guid MediaId, string MediaType) : IQuery<MediaDto>;

internal sealed class GetMediaBlobQueryHandler(ICacheManager cacheManager, IBlobsProvider blobsProvider)
    : IQueryHandler<GetMediaBlobQuery, MediaDto>
{
    public async Task<Result<MediaDto>> HandleAsync(GetMediaBlobQuery query, CancellationToken ct)
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
