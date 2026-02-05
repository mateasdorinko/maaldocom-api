using MaaldoCom.Services.Application.Blobs;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaBlobQuery(ClaimsPrincipal user, string containerName, Guid mediaAlbumId, Guid mediaId, string mediaType)
    : BaseQuery(user), ICommand<Result<MediaDto>>
{
    public string ContainerName { get; } = containerName;
    public Guid MediaAlbumId { get; } = mediaAlbumId;
    public Guid MediaId { get; } = mediaId;
    public string MediaType { get; } = mediaType;
}

public class GetMediaBlobQueryHandler(ICacheManager cacheManager, IBlobsProvider blobsProvider)
    : BaseQueryHandler(cacheManager), ICommandHandler<GetMediaBlobQuery, Result<MediaDto>>
{
    public async Task<Result<MediaDto>> ExecuteAsync(GetMediaBlobQuery query, CancellationToken ct)
    {
        var mediaAlbum = await CacheManager.GetMediaAlbumDetailAsync(query.MediaAlbumId, ct);
        var media = mediaAlbum?.Media.FirstOrDefault(m => m.Id == query.MediaId);
        if (media == null)
        {
            return Result.Fail<MediaDto>(new BlobNotFoundError(query.ContainerName, $"MediaAlbum:{query.MediaAlbumId}/Media:{query.MediaId}"));
        }

        var blobName = query.MediaType is "thumb" or "viewer" ?
            $"{mediaAlbum!.Name}/{query.MediaType}/{query.MediaType}-{media.FileName}" :
            $"{mediaAlbum!.Name}/{query.MediaType}/{media.FileName}";

        var dto = await blobsProvider.GetBlobAsync(query.ContainerName, blobName, ct);

        return dto != null ?
            Result.Ok(dto)! :
            Result.Fail<MediaDto>(new BlobNotFoundError(query.ContainerName, blobName));
    }
}
