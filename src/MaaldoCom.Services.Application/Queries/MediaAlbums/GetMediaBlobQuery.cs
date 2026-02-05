using MaaldoCom.Services.Application.Blobs;
using MaaldoCom.Services.Domain.MediaAlbums;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaBlobQuery(ClaimsPrincipal user, Guid mediaAlbumId, Guid mediaId, string mediaType)
    : BaseQuery(user), ICommand<Result<MediaDto>>
{
    public Guid MediaAlbumId { get; } = mediaAlbumId;
    public Guid MediaId { get; } = mediaId;
    public string MediaType { get; } = mediaType;
}

public class GetMediaBlobQueryHandler(ICacheManager cacheManager, IBlobsProvider blobsProvider)
    : BaseQueryHandler(cacheManager), ICommandHandler<GetMediaBlobQuery, Result<MediaDto>>
{
    public async Task<Result<MediaDto>> ExecuteAsync(GetMediaBlobQuery query, CancellationToken ct)
    {
        const string containerName = "media-albums";
        var mediaAlbum = await CacheManager.GetMediaAlbumDetailAsync(query.MediaAlbumId, ct);
        var media = mediaAlbum?.Media.FirstOrDefault(m => m.Id == query.MediaId);
        if (media == null)
        {
            return Result.Fail<MediaDto>(new BlobNotFoundError(containerName, $"MediaAlbum:{query.MediaAlbumId}/Media:{query.MediaId}"));
        }

        var thumbOrViewerBlobName = $"{mediaAlbum!.UrlFriendlyName}/{query.MediaType}/{query.MediaType}-{MediaAlbumHelper.GetMetaFileExtension(media.FileName!)}";
        var originalBlobName = $"{mediaAlbum.UrlFriendlyName}/{query.MediaType}/{media.FileName}";

        var blobName = query.MediaType is "thumb" or "viewer" ? thumbOrViewerBlobName : originalBlobName;

        var dto = await blobsProvider.GetBlobAsync(containerName, blobName, ct);

        return dto != null ?
            Result.Ok(dto)! :
            Result.Fail<MediaDto>(new BlobNotFoundError(containerName, blobName));
    }
}
