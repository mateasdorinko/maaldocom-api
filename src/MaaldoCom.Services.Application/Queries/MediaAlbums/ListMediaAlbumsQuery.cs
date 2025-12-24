using MaaldoCom.Services.Application.Dtos;

namespace MaaldoCom.Services.Application.Queries.MediaAlbums;

public class ListMediaAlbumsQuery : ICommand<IEnumerable<MediaAlbumDto>> { }

public class ListMediaAlbumsQueryHandler : ICommandHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>
{
    public async Task<IEnumerable<MediaAlbumDto>> ExecuteAsync(ListMediaAlbumsQuery query, CancellationToken cancellationToken)
    {
        var mediaAlbums = new List<MediaAlbumDto>
        {
            new() { 
                Name = "Vacation 2023", 
                UrlFriendlyName = "vacation-2023", 
                Description = "Photos from my 2023 vacation.", 
                Media = new List<MediumDto>
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
                Media = new List<MediumDto>
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
                Media = new List<MediumDto>
                {
                    new() { FileName = "sunset.jpg", Description = "Beautiful sunset." }, 
                    new() { FileName = "forest.jpg", Description = "Lush green forest." }
                }
            },
        };
        
        return await Task.FromResult(mediaAlbums);
    }
}