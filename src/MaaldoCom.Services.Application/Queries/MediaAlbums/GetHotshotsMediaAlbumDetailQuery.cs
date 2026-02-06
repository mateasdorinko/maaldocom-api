namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetHotshotsMediaAlbumDetailQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<MediaAlbumDto>>;

public class GetHotshotsMediaAlbumDetailQueryHandler(ICacheManager cacheManager)
    : BaseQueryHandler(cacheManager), ICommandHandler<GetHotshotsMediaAlbumDetailQuery, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(GetHotshotsMediaAlbumDetailQuery query, CancellationToken ct)
    {
        var dto = await CacheManager.GetHotshotsMediaAlbumDetailAsync(ct);
        return dto!;
    }
}
