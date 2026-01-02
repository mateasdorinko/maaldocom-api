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

    private static async Task<IEnumerable<MediaAlbumDto>> GetMediaAlbumsFromDbAsync(CancellationToken cancellationToken)
    {
        var mediaAlbum1Id = Guid.NewGuid();
        var mediaAlbum2Id = Guid.NewGuid();
        var mediaAlbum3Id = Guid.NewGuid();

        var mediaAlbums = new List<MediaAlbumDto>
        {
            new() {
                Id = mediaAlbum1Id,
                Name = "Vacation 2023",
                UrlFriendlyName = "vacation-2023",
                Description = "Photos from my 2023 vacation.",
                Media = new List<MediaDto>
                {
                    new() { FileName = "beach.jpg", Description = "At the beach.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum1Id, Tags = new List<TagDto> { new TagDto { Name = "alb1tag1"} } },
                    new() { FileName = "mountains.jpg", Description = "Hiking in the mountains.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum1Id, Tags = new List<TagDto> { new TagDto { Name = "alb1tag2"} } }
                },
                Tags = new List<TagDto>
                {
                    new() { Name = "Vacation", Id = Guid.NewGuid() },
                    new() { Name = "2023", Id = Guid.NewGuid() }
                }
            },
            new()
            {
                Id = mediaAlbum2Id,
                Name = "Family Reunion",
                UrlFriendlyName = "family-reunion",
                Description = "Memories from our family reunion.",
                Media = new List<MediaDto>
                {
                    new() { FileName = "group-photo.jpg", Description = "Everyone together.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum2Id, Tags = new List<TagDto> { new TagDto { Name = "alb2tag1"} } },
                    new() { FileName = "dinner.jpg", Description = "Family dinner.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum2Id, Tags = new List<TagDto> { new TagDto { Name = "alb2tag2"} } }
                },
                Tags = new List<TagDto>
                {
                    new() { Name = "Family", Id = Guid.NewGuid() },
                    new() { Name = "Reunion", Id = Guid.NewGuid() }
                }
            },
            new()
            {
                Id = mediaAlbum3Id,
                Name = "Nature Photography",
                UrlFriendlyName = "nature-photography",
                Description = "A collection of nature photographs.",
                Media = new List<MediaDto>
                {
                    new() { FileName = "sunset.jpg", Description = "Beautiful sunset.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum3Id, Tags = new List<TagDto> { new TagDto { Name = "alb3tag1"} } },
                    new() { FileName = "forest.jpg", Description = "Lush green forest.", Id = Guid.NewGuid(), MediaAlbumId = mediaAlbum3Id, Tags = new List<TagDto> { new TagDto { Name = "alb3tag2"} } }
                },
                Tags = new List<TagDto> { new() { Name = "Nature", Id = Guid.NewGuid() } }
            },
        };

        return await Task.FromResult(mediaAlbums);
    }

    /*private async Task<IEnumerable<MediaAlbumDto>> GetMediaAlbumsFromDbAsync(CancellationToken cancellationToken)
    {
        var entities = await MaaldoComDbContext.MediaAlbums.ToListAsync(cancellationToken);
        return entities.ToDtos();
    }*/
}