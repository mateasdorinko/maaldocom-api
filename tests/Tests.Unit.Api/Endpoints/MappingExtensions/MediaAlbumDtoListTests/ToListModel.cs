using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Application.Dtos;

namespace Tests.Unit.Api.Endpoints.MappingExtensions.MediaAlbumDtoListTests;

public class ToListModel
{
    [Fact]
    public void tolistmodel_maps_correctly()
    {
        // Arrange
        var dtos = new List<MediaAlbumDto>
        {
            new()
            {
                Guid = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Active = true,
                Name = "Album 1",
                UrlFriendlyName = "album-1",
                Description = "Description 1",
                Media = new List<MediaDto>()
            },
            new()
            {
                Guid = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Active = false,
                Name = "Album 2",
                UrlFriendlyName = "album-2",
                Description = "Description 2",
                Media = new List<MediaDto>()
            }
        };

        // Act
        var model = dtos.ToListModel();

        // Assert
        var modelMediaAlbums = model.MediaAlbums.ToList();
        modelMediaAlbums.Count.ShouldBe(2);
        modelMediaAlbums[0].Name.ShouldBe("Album 1");
        modelMediaAlbums[1].Name.ShouldBe("Album 2");
    }

    [Fact]
    public void tolistmodel_handles_empty_list()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();
        
        // act
        var model = dtos.ToListModel();
        
        // assert
        model.MediaAlbums.ShouldBeEmpty();
    }

    [Fact]
    public void tolistmodel_handles_null_list()
    {
        // arrange
        List<MediaAlbumDto> dtos = null;
        
        // act & assert
        Should.Throw<ArgumentNullException>(() => dtos.ToListModel());
    }
}