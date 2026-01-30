using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Infrastructure.Cache;

public class CacheManager : ICacheManager
{
    public CacheManager(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    {
        MaaldoComDbContext = maaldoComDbContext;
        HybridCache = hybridCache;

        MaaldoComDbContext.DisableChangeTracking();
    }

    private IMaaldoComDbContext MaaldoComDbContext { get; }
    private HybridCache HybridCache { get; }

    private const string MediaAlbumListCacheKey = "mediaalbums";
    private const string KnowledgeListCacheKey = "knowledge";
    private const string TagListCacheKey = "tags";
    
    public async Task<IEnumerable<MediaAlbumDto>> ListMediaAlbumsAsync(CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            MediaAlbumListCacheKey,
            async _ => await ListMediaAlbumsFromDbAsync(cancellationToken), cancellationToken: cancellationToken);

        return mediaAlbums;
    }

    public async Task<MediaAlbumDto?> GetMediaAlbumDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var cachedMediaAlbum = (await ListMediaAlbumsAsync(cancellationToken)).FirstOrDefault(ma => ma.Id == id);

        if (cachedMediaAlbum == null) { return null; }

        var mediaAlbumDetail = await HybridCache.GetOrCreateAsync<MediaAlbumDto>(
            GetDetailCacheKey(MediaAlbumListCacheKey, id),
            async _ => await GetMediaAlbumDetailFromDbAsync(id, cancellationToken),
            cancellationToken: cancellationToken);

        return mediaAlbumDetail;
    }

    public async Task<IEnumerable<TagDto>> ListTagsAsync(CancellationToken cancellationToken)
    {
        var tags = await HybridCache.GetOrCreateAsync(
            TagListCacheKey,
            async _ => await ListTagsFromDbAsync(cancellationToken), cancellationToken: cancellationToken);

        return tags;
    }

    public async Task<TagDto?> GetTagDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var cachedTag = (await ListTagsAsync(cancellationToken)).FirstOrDefault(t => t.Id == id);

        if (cachedTag == null) { return null; }

        var tagDetail = await HybridCache.GetOrCreateAsync<TagDto>(
            GetDetailCacheKey(TagListCacheKey, id),
            async _ => await GetTagDetailFromDbAsync(id, cancellationToken),
            cancellationToken: cancellationToken);

        return tagDetail;
    }

    public async Task<IEnumerable<KnowledgeDto>> ListKnowledgeAsync(CancellationToken cancellationToken)
    {
        var knowledge = await HybridCache.GetOrCreateAsync(
            KnowledgeListCacheKey,
            async _ => await ListKnowledgeFromDbAsync(cancellationToken), cancellationToken: cancellationToken);

        return knowledge;
    }

    public async Task RefreshCacheAsync(CancellationToken cancellationToken)
    {
        await ListMediaAlbumsAsync(cancellationToken);
        await ListTagsAsync(cancellationToken);
        await ListKnowledgeAsync(cancellationToken);
    }

    private static string GetDetailCacheKey(string listCacheKey, Guid detailId) => $"{listCacheKey}:{detailId}";
    
    private async Task<IEnumerable<MediaAlbumDto>> ListMediaAlbumsFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.MediaAlbums
            .Include(ma => ma.MediaAlbumTags)
            .ThenInclude(mat => mat.Tag)
            .ToListAsync(cancellationToken);

        return entities.ToDtos();
    }

    private async Task<MediaAlbumDto> GetMediaAlbumDetailFromDbAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await MaaldoComDbContext.MediaAlbums
            .Include(ma => ma.MediaAlbumTags)
            .ThenInclude(mat => mat.Tag)
            .Include(ma => ma.Media)
            .ThenInclude(m => m.MediaTags)
            .ThenInclude(mt => mt.Tag)
            .FirstOrDefaultAsync(ma => ma.Id == id, cancellationToken);

        return entity!.ToDto();
    }

    private async Task<IEnumerable<TagDto>> ListTagsFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.Tags.ToListAsync(cancellationToken);

        return entities.ToDtos();
    }

    private async Task<TagDto> GetTagDetailFromDbAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await MaaldoComDbContext.Tags
            .Where(t => t.Id == id)
            .Include(t => t.MediaAlbumTags)
            .ThenInclude(mat => mat.MediaAlbum)
            .Include(t => t.MediaTags)
            .ThenInclude(mt => mt.Media)
            .ThenInclude(m => m.MediaAlbum)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        return entity!.ToDto();
    }

    private async Task<IEnumerable<KnowledgeDto>> ListKnowledgeFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.Knowledge.ToListAsync(cancellationToken);

        return entities.ToDtos();
    }
}