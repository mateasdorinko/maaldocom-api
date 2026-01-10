namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToModel
{
    [Fact]
    public void ToModel_FromMediaAlbumDto_MapsAllPropertiesCorrectly()
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
        var model = dto.ToModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Name.ShouldBeEquivalentTo(dto.Name);
        model.UrlFriendlyName.ShouldBeEquivalentTo(dto.UrlFriendlyName);
        model.Created.ShouldBeEquivalentTo(dto.Created);
        model.Tags.Count().ShouldBe(1);
        model.Tags.First().ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToModel_FromNullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModel_FromMediaDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new MediaDto
        {
            Id = Guid.NewGuid(),
            MediaAlbumId = Guid.NewGuid(),
            FileName = "sample.jpg",
            Description = "This is a sample media file.",
            SizeInBytes = 2048,
            FileExtension = ".jpg",
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
            }
        };

        // act
        var model = dto.ToModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.FileName.ShouldBeEquivalentTo(dto.FileName);
        model.Description.ShouldBeEquivalentTo(dto.Description);
    }

    [Fact]
    public void ToModel_FromNullMediaDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModel_FromTagDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new TagDto
        {
            Id = Guid.NewGuid(),
            Name = "SampleTag"
        };

        // act
        var model = dto.ToModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Name.ShouldBeEquivalentTo(dto.Name);
    }

    [Fact]
    public void ToModel_FromNullTagDto_ThrowsArgumentNullException()
    {
        // arrange
        TagDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModel_FromKnowledgeDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new KnowledgeDto
        {
            Id = Guid.NewGuid(),
            Title = "Sample Knowledge",
            Quote = "This is a sample knowledge content.",
            CreatedBy = "tester",
            Created = DateTime.UtcNow,
            LastModifiedBy = "tester",
            LastModified = DateTime.UtcNow,
            Active = true
        };

        // act
        var model = dto.ToModel();

        // assert
        model.Id.ShouldBeEquivalentTo(dto.Id);
        model.Title.ShouldBeEquivalentTo(dto.Title);
        model.Quote.ShouldBeEquivalentTo(dto.Quote);
    }

    [Fact]
    public void ToModel_FromNullKnowledgeDto_ThrowsArgumentNullException()
    {
        // arrange
        KnowledgeDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }
}