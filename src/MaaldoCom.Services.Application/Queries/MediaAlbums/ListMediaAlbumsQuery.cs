using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class ListMediaAlbumsQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<Result<IEnumerable<MediaAlbumDto>>> { }

public class ListMediaAlbumsQueryHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    : BaseQueryHandler(maaldoComDbContext, hybridCache), ICommandHandler<ListMediaAlbumsQuery, Result<IEnumerable<MediaAlbumDto>>>
{
    public async Task<Result<IEnumerable<MediaAlbumDto>>> ExecuteAsync(ListMediaAlbumsQuery query, CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            MediaAlbumListCacheKey,
            async _ => await GetMediaAlbumsFromDbAsync(cancellationToken), cancellationToken: cancellationToken);

        var activeMediaAlbums = mediaAlbums.Where(ma => ma.Active);

        return Result.Ok(activeMediaAlbums);
    }

    private async Task<IEnumerable<MediaAlbumDto>> GetMediaAlbumsFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.MediaAlbums
            .Include(ma => ma.MediaAlbumTags)
            .ThenInclude(mat => mat.Tag)
            .ToListAsync(cancellationToken);

        return entities.ToDtos();
    }
}