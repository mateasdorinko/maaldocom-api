namespace Tests.Unit.Application.Extensions.MapperExtensionsTests;

public class ToEntity
{
    [Fact]
    public void ToEntity_FromMediaAlbumDto_MapsAllPropertiesCorrectly()
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
        var entity = dto.ToEntity();
        
        // assert
        entity.Id.ShouldBeEquivalentTo(dto.Id);
        entity.Name.ShouldBeEquivalentTo(dto.Name);
        entity.UrlFriendlyName.ShouldBeEquivalentTo(dto.UrlFriendlyName);
        entity.Description.ShouldBeEquivalentTo(dto.Description);
        entity.CreatedBy.ShouldBeEquivalentTo(dto.CreatedBy);
        entity.Created.ShouldBeEquivalentTo(dto.Created);
        entity.LastModifiedBy.ShouldBeEquivalentTo(dto.LastModifiedBy);
        entity.LastModified.ShouldBeEquivalentTo(dto.LastModified);
        entity.Active.ShouldBeEquivalentTo(dto.Active);
        entity.MediaAlbumTags.Count.ShouldBe(1);
        entity.MediaAlbumTags.First().Tag.Name.ShouldBeEquivalentTo("SampleTag");
        entity.Media.Count.ShouldBe(1);
        entity.Media.First().FileName.ShouldBeEquivalentTo("sample.jpg");
    }

    [Fact]
    public void ToEntity_FromNullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToEntity());
    }

    [Fact]
    public void ToEntity_FromMediaDto_MapsAllPropertiesCorrectly()
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
        var entity = dto.ToEntity();
        
        // assert
        entity.Id.ShouldBeEquivalentTo(dto.Id);
        entity.MediaAlbumId.ShouldBeEquivalentTo(dto.MediaAlbumId);
        entity.FileName.ShouldBeEquivalentTo(dto.FileName);
        entity.Description.ShouldBeEquivalentTo(dto.Description);
        entity.SizeInBytes.ShouldBeEquivalentTo(dto.SizeInBytes);
        entity.FileExtension.ShouldBeEquivalentTo(dto.FileExtension);
        entity.CreatedBy.ShouldBeEquivalentTo(dto.CreatedBy);
        entity.Created.ShouldBeEquivalentTo(dto.Created);
        entity.LastModifiedBy.ShouldBeEquivalentTo(dto.LastModifiedBy);
        entity.LastModified.ShouldBeEquivalentTo(dto.LastModified);
        entity.Active.ShouldBeEquivalentTo(dto.Active);
        entity.MediaTags.Count.ShouldBe(1);
        entity.MediaTags.First().Tag.Name.ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToEntity_FromNullMediaDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaDto? dto = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToEntity());
    }

    [Fact]
    public void ToEntity_FromTagDto_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dto = new TagDto
        {
            Id = Guid.NewGuid(),
            Name = "SampleTag"
        };
        
        // act
        var entity = dto.ToEntity();
        
        // assert
        entity.Id.ShouldBeEquivalentTo(dto.Id);
        entity.Name.ShouldBeEquivalentTo(dto.Name);
    }

    [Fact]
    public void ToEntity_FromNullTagDto_ThrowsArgumentNullException()
    {
        // arrange
        TagDto? dto = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToEntity());
    }
    
    [Fact]
    public void ToEntity_FromKnowledgeDto_MapsAllPropertiesCorrectly()
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
        var entity = dto.ToEntity();
        
        // assert
        entity.Id.ShouldBeEquivalentTo(dto.Id);
        entity.Title.ShouldBeEquivalentTo(dto.Title);
        entity.Quote.ShouldBeEquivalentTo(dto.Quote);
    }

    [Fact]
    public void ToEntity_FromNullKnowledgeDto_ThrowsArgumentNullException()
    {
        // arrange
        KnowledgeDto? dto = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToEntity());
    }
}