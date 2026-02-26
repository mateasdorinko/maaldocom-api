using MaaldoCom.Api.Application.Dtos;

namespace MaaldoCom.Api.Application.Cache;

public interface ICacheManager
{
    Task<IEnumerable<MediaAlbumDto>> ListMediaAlbumsAsync(CancellationToken cancellationToken);
    Task<MediaAlbumDto?> GetMediaAlbumDetailAsync(Guid id, CancellationToken cancellationToken);
    Task<MediaAlbumDto?> GetHotshotsMediaAlbumDetailAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TagDto>> ListTagsAsync(CancellationToken cancellationToken);
    Task<TagDto?> GetTagDetailAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<KnowledgeDto>> ListKnowledgeAsync(CancellationToken cancellationToken);

    Task RefreshCacheAsync(CancellationToken cancellationToken);
    Task InvalidateCache(string cacheKey, CancellationToken cancellationToken);
}
