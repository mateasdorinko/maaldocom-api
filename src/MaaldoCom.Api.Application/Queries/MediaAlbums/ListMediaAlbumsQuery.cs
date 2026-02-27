namespace MaaldoCom.Api.Application.Queries.MediaAlbums;

public sealed record ListMediaAlbumsQuery : IQuery<IEnumerable<MediaAlbumDto>>;

internal sealed class ListMediaAlbumsQueryHandler(ICacheManager cacheManager) : IQueryHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>
{
    public async Task<Result<IEnumerable<MediaAlbumDto>>> HandleAsync(ListMediaAlbumsQuery query, CancellationToken ct)
    {
        var mediaAlbums = await cacheManager.ListMediaAlbumsAsync(ct);
        var activeMediaAlbums = mediaAlbums.Where(ma => ma.Active);

        return Result.Ok(activeMediaAlbums);
    }
}
