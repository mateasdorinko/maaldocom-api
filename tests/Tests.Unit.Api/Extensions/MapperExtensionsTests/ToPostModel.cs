namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToPostModel
{
    [Fact]
    public void ToPostModel_FromMediaAlbumDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            UrlFriendlyName = "sample-album",
            Created = DateTime.UtcNow,
            Tags = new List<TagDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "SampleTag"
                }
            }
        };

        // act
        var model = dto.ToPostModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Name.ShouldBeEquivalentTo(dto.Name);
        model.UrlFriendlyName.ShouldBeEquivalentTo(dto.UrlFriendlyName);
        model.Created.ShouldBeEquivalentTo(dto.Created);
        model.Tags.Count().ShouldBe(1);
        model.Tags.First().ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToPostModel_FromNullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToPostModel());
    }
}
