using System.Security.Claims;
using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class ListMediaAlbumsQueryCommand(ClaimsPrincipal user) : BaseQueryCommand(user), ICommand<IEnumerable<MediaAlbumDto>> { }

public class ListMediaAlbumsQueryCommandHandler(IMaaldoComDbContext maaldoComDbContext, HybridCache hybridCache)
    : BaseQueryCommandHandler(maaldoComDbContext, hybridCache), ICommandHandler<ListMediaAlbumsQueryCommand, IEnumerable<MediaAlbumDto>>
{
    public async Task<IEnumerable<MediaAlbumDto>> ExecuteAsync(ListMediaAlbumsQueryCommand queryCommand, CancellationToken cancellationToken)
    {
        var mediaAlbums = await HybridCache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>(
            "media-albums",
            async _ => await GetStaticMediaAlbums(), cancellationToken: cancellationToken);

        return mediaAlbums;
    }

    private async Task<IEnumerable<MediaAlbumDto>> GetStaticMediaAlbums()
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