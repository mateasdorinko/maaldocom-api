namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public sealed record GetHotshotsMediaAlbumDetailQuery : IQuery<MediaAlbumDto>;

internal sealed class GetHotshotsMediaAlbumDetailQueryHandler(ICacheManager cacheManager)
    : IQueryHandler<GetHotshotsMediaAlbumDetailQuery, MediaAlbumDto>
{
    public async Task<Result<MediaAlbumDto>> HandleAsync(GetHotshotsMediaAlbumDetailQuery query, CancellationToken ct)
    {
        var dto = await cacheManager.GetHotshotsMediaAlbumDetailAsync(ct);
        return dto!;
    }
}
