namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetHotshotsMediaAlbumDetailQuery : ICommand<Result<MediaAlbumDto>>;

public class GetHotshotsMediaAlbumDetailQueryHandler(ICacheManager cacheManager)
    : ICommandHandler<GetHotshotsMediaAlbumDetailQuery, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(GetHotshotsMediaAlbumDetailQuery query, CancellationToken ct)
    {
        var dto = await cacheManager.GetHotshotsMediaAlbumDetailAsync(ct);
        return dto!;
    }
}
