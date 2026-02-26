using MaaldoCom.Api.Domain.MediaAlbums;
using MaaldoCom.Api.Infrastructure.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Api.Infrastructure.Cache;

public sealed class CacheManager : ICacheManager, IDisposable
{
    private MaaldoComDbContext MaaldoComDbContext { get; }
    private HybridCache HybridCache { get; }

    public CacheManager(IDbContextFactory<MaaldoComDbContext> dbContextFactory, HybridCache hybridCache)
    {
        MaaldoComDbContext = dbContextFactory.CreateDbContext();
        MaaldoComDbContext.DisableChangeTracking();
        HybridCache = hybridCache;
    }

    public void Dispose()
    {
        MaaldoComDbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<MediaAlbumDto>> ListMediaAlbumsAsync(CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            CacheKeys.MediaAlbumList,
            async _ => await GetFromDbAsync(), cancellationToken: cancellationToken);

        return mediaAlbums;

        async Task<IEnumerable<MediaAlbumDto>> GetFromDbAsync()
        {
            var entities = await MaaldoComDbContext.MediaAlbums.Include(ma => ma.MediaAlbumTags)
                .ThenInclude(mat => mat.Tag)
                .Include(ma => ma.Media.OrderBy(m => m.Created).Take(1))
                .OrderByDescending(ma => ma.Created)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return entities.ToDtos();
        }
    }

    public async Task<MediaAlbumDto?> GetMediaAlbumDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var cachedMediaAlbum = (await ListMediaAlbumsAsync(cancellationToken)).FirstOrDefault(ma => ma.Id == id);

        if (cachedMediaAlbum == null) { return null; }

        var mediaAlbumDetail = await HybridCache.GetOrCreateAsync<MediaAlbumDto>(
            GetDetailCacheKey(CacheKeys.MediaAlbumList, id),
            async _ => await GetFromDbAsync(),
            cancellationToken: cancellationToken);

        return mediaAlbumDetail;

        async Task<MediaAlbumDto> GetFromDbAsync()
        {
            var entity = await MaaldoComDbContext.MediaAlbums
                .Include(ma => ma.MediaAlbumTags)
                .ThenInclude(mat => mat.Tag)
                .Include(ma => ma.Media.OrderBy(m => m.FileName))
                .ThenInclude(m => m.MediaTags)
                .ThenInclude(mt => mt.Tag)
                .AsSplitQuery()
                .FirstOrDefaultAsync(ma => ma.Id == id, cancellationToken);

            var dto = entity!.ToDto();
            SetMediaContentType(dto);

            return dto;
        }
    }

    public async Task<MediaAlbumDto?> GetHotshotsMediaAlbumDetailAsync(CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<MediaAlbumDto>(
            $"{CacheKeys.MediaAlbumList}:hotshots",
            async _ => await GetFromDbAsync(), cancellationToken: cancellationToken);

        return mediaAlbums;

        async Task<MediaAlbumDto> GetFromDbAsync()
        {
            var entity = await MaaldoComDbContext.MediaAlbums
                .Where(ma => ma.UrlFriendlyName == "hotshots")
                .Include(ma => ma.MediaAlbumTags)
                .ThenInclude(mat => mat.Tag)
                .Include(ma => ma.Media.OrderBy(m => m.FileName))
                .ThenInclude(m => m.MediaTags)
                .ThenInclude(mt => mt.Tag)
                .AsSplitQuery()
                .FirstOrDefaultAsync(cancellationToken);

            var dto = entity!.ToDto();
            SetMediaContentType(dto);

            var taggedMedia = await MaaldoComDbContext.Media
                .Where(m => m.MediaAlbumId != entity!.Id && m.MediaTags.Any(mt => mt.Tag.Name == "hotshots"))
                .OrderBy(m => m.Created)
                .ToListAsync(cancellationToken);

            foreach (var media in taggedMedia) { dto.Media.Add(media.ToDto()); }

            return dto;
        }
    }

    public async Task<IEnumerable<TagDto>> ListTagsAsync(CancellationToken cancellationToken)
    {
        var tags = await HybridCache.GetOrCreateAsync(
            CacheKeys.TagList,
            async _ => await GetFromDbAsync(), cancellationToken: cancellationToken);

        return tags;

        async Task<IEnumerable<TagDto>> GetFromDbAsync()
        {
            var entities = await MaaldoComDbContext.Tags
                .Include(t => t.MediaAlbumTags)
                .Include(t => t.MediaTags)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return entities.ToDtos().OrderByDescending(t => t.Count);
        }
    }

    public async Task<TagDto?> GetTagDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var cachedTag = (await ListTagsAsync(cancellationToken)).FirstOrDefault(t => t.Id == id);

        if (cachedTag == null) { return null; }

        var tagDetail = await HybridCache.GetOrCreateAsync<TagDto>(
            GetDetailCacheKey(CacheKeys.TagList, id),
            async _ => await GetFromDbAsync(),
            cancellationToken: cancellationToken);

        return tagDetail;

        async Task<TagDto> GetFromDbAsync()
        {
            var entity = await MaaldoComDbContext.Tags
                .Where(t => t.Id == id)
                .Include(t => t.MediaAlbumTags.OrderBy(mat => mat.MediaAlbum.Created))
                .ThenInclude(mat => mat.MediaAlbum)
                .Include(t => t.MediaTags.OrderByDescending(mt => mt.Media.Created))
                .ThenInclude(mt => mt.Media)
                .ThenInclude(m => m.MediaAlbum)
                .AsSplitQuery()
                .FirstOrDefaultAsync(cancellationToken);

            return entity!.ToDto();
        }
    }

    public async Task<IEnumerable<KnowledgeDto>> ListKnowledgeAsync(CancellationToken cancellationToken)
    {
        var knowledge = await HybridCache.GetOrCreateAsync(
            CacheKeys.KnowledgeList,
            async _ => await GetFromDbAsync(), cancellationToken: cancellationToken);

        return knowledge;

        async Task<IEnumerable<KnowledgeDto>> GetFromDbAsync()
        {
            var entities = await MaaldoComDbContext.Knowledge.ToListAsync(cancellationToken);

            return entities.ToDtos();
        }
    }

    public async Task RefreshCacheAsync(CancellationToken cancellationToken)
    {
        await ListMediaAlbumsAsync(cancellationToken);
        await ListTagsAsync(cancellationToken);
        await ListKnowledgeAsync(cancellationToken);
        await GetHotshotsMediaAlbumDetailAsync(cancellationToken);
    }

    public async Task InvalidateCache(string cacheKey, CancellationToken cancellationToken) => await HybridCache.RemoveAsync(cacheKey, cancellationToken);

    private static string GetDetailCacheKey(string listCacheKey, Guid detailId) => $"{listCacheKey}:{detailId}";

    private static void SetMediaContentType(MediaAlbumDto dto)
    {
        foreach (var media in dto.Media)
        {
            if (MediaAlbumHelper.IsPic(media.FileName!)) { media.ContentType = "image"; }
            if (MediaAlbumHelper.IsVid(media.FileName!)) { media.ContentType = "video";}
        }
    }
}
