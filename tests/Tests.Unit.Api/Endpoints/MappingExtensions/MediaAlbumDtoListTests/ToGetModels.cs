using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Application.Dtos;

namespace Tests.Unit.Api.Endpoints.MappingExtensions.MediaAlbumDtoListTests;

public class ToGetModels
{
    [Fact]
    public void togetmodels_with_empty_list_returns_empty_list()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();

        // act
        var models = dtos.ToGetModels();

        // assert
        models.ShouldBeEmpty();
    }

    [Fact(Skip =  "Not implemented")]
    public void togetmodels_with_null_returns_empty_list()
    {
        // arrange
        List<MediaAlbumDto> dtos = null;

        // act
        var models = dtos.ToGetModels();

        // assert
        models.ShouldBeEmpty();
    }

    [Fact]
    public void togetmodels_with_multiple_items_maps_all_items()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>
        {
            new()
            {
                Guid = Guid.NewGuid(),
                Name = "Album 1",
                UrlFriendlyName = "album-1",
                Description = "Description 1",
                Created = DateTime.UtcNow,
                Active = true
            },
            new()
            {
                Guid = Guid.NewGuid(),
                Name = "Album 2",
                UrlFriendlyName = "album-2",
                Description = "Description 2",
                Created = DateTime.UtcNow,
                Active = false
            }
        };

        // act
        var models = dtos.ToGetModels();

        // assert
        models.Count().ShouldBe(2);
    }

    [Fact]
    public void togetmodels_includes_media_when_specified()
    {
        // arrange
        var dto = new MediaAlbumDto
        {
            Guid = Guid.NewGuid(),
            Name = "Album with Media",
            UrlFriendlyName = "album-with-media",
            Description = "Description with Media",
            Created = DateTime.UtcNow,
            Active = true,
            Media = new List<MediaDto>
            {
                new()
                {
                    Guid = Guid.NewGuid(),
                    FileName = "media1.jpg",
                    Description = "Media 1",
                    Created = DateTime.UtcNow,
                    Active = true
                },
                new()
                {
                    Guid = Guid.NewGuid(),
                    FileName = "media2.jpg",
                    Description = "Media 2",
                    Created = DateTime.UtcNow,
                    Active = false
                }
            }
        };

        // act
        var models = (new List<MediaAlbumDto> { dto }.ToGetModels(true)).ToList();

        // assert
        models.Count.ShouldBe(1);
        var model = models.First();
        model.Media.Count.ShouldBe(2);
    }
}