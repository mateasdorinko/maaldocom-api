namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToDetailModel
{
    [Fact]
    public void ToDetailModel_FromMediaAlbumDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            UrlFriendlyName = "sample-album",
            Description = "This is a sample media album.",
            CreatedBy = "tester",
            Created = DateTime.UtcNow,
            LastModifiedBy = "tester",
            LastModified = DateTime.UtcNow,
            Active = true,
            Tags = new List<TagDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "SampleTag"
                }
            },
            Media = new List<MediaDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "sample.jpg",
                    Description = "This is a sample media file.",
                    SizeInBytes = 2048,
                    FileExtension = ".jpg",
                    CreatedBy = "tester",
                    Created = DateTime.UtcNow,
                    LastModifiedBy = "tester",
                    LastModified = DateTime.UtcNow,
                    Active = true
                }
            }
        };

        // act
        var model = dto.ToDetailModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Name.ShouldBeEquivalentTo(dto.Name);
        model.Description.ShouldBeEquivalentTo(dto.Description);
        model.UrlFriendlyName.ShouldBeEquivalentTo(dto.UrlFriendlyName);
        model.Created.ShouldBeEquivalentTo(dto.Created);
        model.Active.ShouldBeEquivalentTo(dto.Active);
        model.Media.Count().ShouldBe(1);
        model.Media.First().FileName.ShouldBeEquivalentTo("sample.jpg");
        model.Tags.Count().ShouldBe(1);
        model.Tags.First().ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToDetailModel_FromNullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToDetailModel());
    }

    [Fact]
    public void ToDetailModel_FromTagDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new TagDto
        {
            Id = Guid.NewGuid(),
            Name = "SampleTag",
            CreatedBy = "tester",
            Created = DateTime.UtcNow,
            LastModifiedBy = "tester",
            LastModified = DateTime.UtcNow,
            MediaAlbums = new List<MediaAlbumDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sample Album"
                }
            },
            Media = new List<MediaDto>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "sample.jpg",
                    MediaAlbumId = Guid.NewGuid(),
                    MediaAlbumName = "Sample Album"
                }
            }
        };

        // act
        var model = dto.ToDetailModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Name.ShouldBeEquivalentTo(dto.Name);
        model.MediaAlbums.Count().ShouldBe(1);
        model.MediaAlbums.First().Name.ShouldBeEquivalentTo("Sample Album");
        model.Media.Count().ShouldBe(1);
        model.Media.First().Name.ShouldBeEquivalentTo("sample.jpg");
        model.Media.First().MediaAlbumName.ShouldBeEquivalentTo("Sample Album");
    }

    [Fact]
    public void ToDetailModel_FromNullTagDto_ThrowsArgumentNullException()
    {
        // arrange
        TagDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToDetailModel());
    }
}