using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Application.Dtos;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Queries.MediaAlbums;

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
