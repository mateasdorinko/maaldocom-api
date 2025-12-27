using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Application.Dtos;

namespace Tests.Unit.Api.Endpoints.MappingExtensions.MediaAlbumDtoTests;

public class ToGetModel
{
    [Fact]
    public void togetmodel_maps_all_properties_correctly_without_media()
    {
        // arrange
        var dto = new MediaAlbumDto
        {
            Id = 5678,
            Guid = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            Active = true,
            Name = "Holiday Photos",
            UrlFriendlyName = "holiday-photos",
            Description = "Photos from my holiday",
            Media = new List<MediaDto>
            {
                new() { Id = 1, FileName = "beach.jpg" },
                new() { Id = 2, FileName = "mountains.jpg" }
            }
        };
        
        // act
        var model = dto.ToGetModel();
        
        // assert
        dto.Guid.ShouldBe(model.Guid);
        dto.Created.ShouldBe(model.Created);
        dto.Active.ShouldBe(model.Active);
        dto.Name.ShouldBe(model.Name);
        dto.UrlFriendlyName.ShouldBe(model.UrlFriendlyName);
        dto.Description.ShouldBe(model.Description);
        model.Media.ShouldBeEmpty();
    }
    
    [Fact]
    public void togetmodel_maps_all_properties_correctly_with_media()
    {
        // arrange
        var dto = new MediaAlbumDto
        {
            Id = 5678,
            Guid = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            Active = true,
            Name = "Holiday Photos",
            UrlFriendlyName = "holiday-photos",
            Description = "Photos from my holiday",
            Media = new List<MediaDto>
            {
                new() { Id = 1, FileName = "beach.jpg" },
                new() { Id = 2, FileName = "mountains.jpg" }
            }
        };
        
        // act
        var model = dto.ToGetModel(true);
        
        // assert
        dto.Guid.ShouldBe(model.Guid);
        dto.Created.ShouldBe(model.Created);
        dto.Active.ShouldBe(model.Active);
        dto.Name.ShouldBe(model.Name);
        dto.UrlFriendlyName.ShouldBe(model.UrlFriendlyName);
        dto.Description.ShouldBe(model.Description);
        model.Media.Count.ShouldBe(model.Media.Count);
    }
}