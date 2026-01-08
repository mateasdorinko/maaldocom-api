using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Application.Cache;

public interface ICacheManager
{
    Task<IEnumerable<MediaAlbumDto>> ListMediaAlbumsAsync(CancellationToken cancellationToken);
    Task<MediaAlbumDto?> GetMediaAlbumDetailAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TagDto>> ListTagsAsync(CancellationToken cancellationToken);
    Task<TagDto?> GetTagDetailAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<KnowledgeDto>> ListKnowledgeAsync(CancellationToken cancellationToken);
}