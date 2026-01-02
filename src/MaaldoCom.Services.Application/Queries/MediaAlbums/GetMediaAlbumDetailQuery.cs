using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Errors;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class GetMediaAlbumDetailQuery : BaseQuery, ICommand<Result<MediaAlbumDto>>
{
    public GetMediaAlbumDetailQuery(ClaimsPrincipal user, string name) : base(user)
    {
        Name = name;
    }

    public GetMediaAlbumDetailQuery(ClaimsPrincipal user, Guid id) : base(user)
    {
        Id = id;
    }

    public Guid? Id { get; }
    public string? Name { get; }
}

public class GetMediaAlbumDetailQueryHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    : BaseQueryHandler(maaldoComDbContext, hybridCache), ICommandHandler<GetMediaAlbumDetailQuery, Result<MediaAlbumDto>>
{
    public async Task<Result<MediaAlbumDto>> ExecuteAsync(GetMediaAlbumDetailQuery query, CancellationToken cancellationToken)
    {
        var cachedMediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            MediaAlbumListCacheKey,
            async _ => await GetMediaAlbumsFromDbAsync(cancellationToken), cancellationToken: cancellationToken);

        var searchBy = query.Name ?? query.Id.ToString();
        var searchValue = query.Name ?? query.Id.ToString();

        var cachedMediaAlbum = cachedMediaAlbums.FirstOrDefault(ma => ma.Id == query.Id || ma.Name == query.Name);

        if (cachedMediaAlbum == null)
        {
            return Result.Fail<MediaAlbumDto>(new EntityNotFound("MediaAlbum", searchBy!, query.Id!));
        }

        var mediaAlbumDetail = await HybridCache.GetOrCreateAsync<MediaAlbumDto>(
            $"{MediaAlbumListCacheKey}:{searchValue}",
            async _ => (await GetMediaAlbumDetailFromDbAsync(cachedMediaAlbum.Id, cancellationToken))!, cancellationToken: cancellationToken);

        return Result.Ok(mediaAlbumDetail);
    }

    private async Task<IEnumerable<MediaAlbumDto>> GetMediaAlbumsFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.MediaAlbums
            .Include(ma => ma.MediaAlbumTags)
            .ThenInclude(mat => mat.Tag)
            .ToListAsync(cancellationToken);

        return entities.ToDtos();
    }

    private async Task<MediaAlbumDto?> GetMediaAlbumDetailFromDbAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await MaaldoComDbContext.MediaAlbums
            .Include(ma => ma.MediaAlbumTags)
            .ThenInclude(mat => mat.Tag)
            .FirstOrDefaultAsync(ma => ma.Id == id, cancellationToken);

        return entity?.ToDto();
    }
}