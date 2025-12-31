using System.Security.Claims;
using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class ListMediaAlbumsQuery(ClaimsPrincipal user) : BaseQuery(user), ICommand<IEnumerable<MediaAlbumDto>> { }

public class ListMediaAlbumsQueryHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    : BaseQueryHandler(maaldoComDbContext, hybridCache), ICommandHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>
{
    public async Task<IEnumerable<MediaAlbumDto>> ExecuteAsync(ListMediaAlbumsQuery query, CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            "media-albums-list",
            async _ => await GetStaticMediaAlbums(), cancellationToken: cancellationToken);

        return mediaAlbums;
    }

    private static async Task<IEnumerable<MediaAlbumDto>> GetStaticMediaAlbums()
    {
        var mediaAlbums = new List<MediaAlbumDto>
        {
            new() { 
                Name = "Vacation 2023", 
                UrlFriendlyName = "vacation-2023", 
                Description = "Photos from my 2023 vacation.", 
                Media = new List<MediaDto>
                {
                    new() { FileName = "beach.jpg", Description = "At the beach." }, 
                    new() { FileName = "mountains.jpg", Description = "Hiking in the mountains." }
                }
            },
            new()
            {
                Name = "Family Reunion", 
                UrlFriendlyName = "family-reunion", 
                Description = "Memories from our family reunion.",
                Media = new List<MediaDto>
                {
                    new() { FileName = "group-photo.jpg", Description = "Everyone together." }, 
                    new() { FileName = "dinner.jpg", Description = "Family dinner." }
                }
            },
            new()
            {
                Name = "Nature Photography", 
                UrlFriendlyName = "nature-photography", 
                Description = "A collection of nature photographs.",
                Media = new List<MediaDto>
                {
                    new() { FileName = "sunset.jpg", Description = "Beautiful sunset." }, 
                    new() { FileName = "forest.jpg", Description = "Lush green forest." }
                }
            },
        };
        
        return await Task.FromResult(mediaAlbums);
    }
}