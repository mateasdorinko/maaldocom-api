namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToDetailModels
{
    [Fact]
    public void ToDetailModels_FromMediaAlbumDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sample Album 1",
                UrlFriendlyName = "sample-album-1",
                Created = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sample Album 2",
                UrlFriendlyName = "sample-album-2",
                Created = DateTime.UtcNow
            }
        };

        // act
        var models = dtos.ToDetailModels().ToList();

        // assert
        models.Count.ShouldBe(2);
        models[0].Id.ShouldBeEquivalentTo(dtos[0].Id);
        models[0].Name.ShouldBeEquivalentTo(dtos[0].Name);
        models[1].Id.ShouldBeEquivalentTo(dtos[1].Id);
        models[1].Name.ShouldBeEquivalentTo(dtos[1].Name);
    }

    [Fact]
    public void ToDetailModels_FromNullMediaAlbumDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbumDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToDetailModels());
    }

    [Fact]
    public void ToDetailModels_FromEmptyMediaAlbumDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();

        // act
        var models = dtos.ToDetailModels().ToList();

        // assert
        models.ShouldBeEmpty();
    }
}