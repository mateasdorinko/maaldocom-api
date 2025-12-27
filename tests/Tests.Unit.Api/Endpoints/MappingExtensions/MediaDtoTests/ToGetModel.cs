using MaaldoCom.Services.Api.Endpoints;
using MaaldoCom.Services.Application.Dtos;

namespace Tests.Unit.Api.Endpoints.MappingExtensions.MediaDtoTests;

public class ToGetModel
{
    [Fact]
    public void togetmodel_maps_all_properties_correctly()
    {
        // arrange
        var dto = new MediaDto
        {
            Id = 1234,
            Guid = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            Active = true,
            FileName = "example.jpg",
            Description = "An example media file"
        };

        // act
        var model = dto.ToGetModel();

        // assert
        dto.Guid.ShouldBe(model.Guid);
        dto.Created.ShouldBe(model.Created);
        dto.Active.ShouldBe(model.Active);
        dto.FileName.ShouldBe(model.FileName);
        dto.Description.ShouldBe(model.Description);
    }
}