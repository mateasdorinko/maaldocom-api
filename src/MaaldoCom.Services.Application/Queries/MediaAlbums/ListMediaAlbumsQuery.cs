namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class ListMediaAlbumsQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<IEnumerable<MediaAlbumDto>>> { }

public class ListMediaAlbumsQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<ListMediaAlbumsQuery, Result<IEnumerable<MediaAlbumDto>>>
{
    public async Task<Result<IEnumerable<MediaAlbumDto>>> ExecuteAsync(ListMediaAlbumsQuery query, CancellationToken ct)
    {
        var mediaAlbums = await CacheManager.ListMediaAlbumsAsync(ct);
        var activeMediaAlbums = mediaAlbums.Where(ma => ma.Active);

        return Result.Ok(activeMediaAlbums);
    }
}
