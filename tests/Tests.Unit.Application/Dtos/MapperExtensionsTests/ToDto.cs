namespace Tests.Unit.Application.Dtos.MapperExtensionsTests;

public class ToDto
{
    [Fact]
    public void ToDto_MappingMediaAlbumEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new MediaAlbum
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
            Tags = new List<MediaAlbumTag>
            {
                new()
                {
                    Tag = new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "SampleTag"
                    }
                }
            },
            Media = new List<Media>
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
        var dto = entity.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.Name.ShouldBeEquivalentTo(entity.Name);
        dto.UrlFriendlyName.ShouldBeEquivalentTo(entity.UrlFriendlyName);
        dto.Description.ShouldBeEquivalentTo(entity.Description);
        dto.CreatedBy.ShouldBeEquivalentTo(entity.CreatedBy);
        dto.Created.ShouldBeEquivalentTo(entity.Created);
        dto.LastModifiedBy.ShouldBeEquivalentTo(entity.LastModifiedBy);
        dto.LastModified.ShouldBeEquivalentTo(entity.LastModified);
        dto.Active.ShouldBeEquivalentTo(entity.Active);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
        dto.Media.Count.ShouldBe(1);
        dto.Media.First().FileName.ShouldBeEquivalentTo("sample.jpg");
    }

    [Fact]
    public void ToDto_NullMediaAlbumEntity_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbum? entity = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entity!.ToDto());
    }

    [Fact]
    public void ToDto_MappingMediaEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new Media
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
            Tags = new List<MediaTag>
            {
                new()
                {
                    Tag = new Tag
                    {
                        Id = Guid.NewGuid(),
                        Name = "SampleTag"
                    }
                }
            }
        };
        
        // act
        var dto = entity.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.MediaAlbumId.ShouldBeEquivalentTo(entity.MediaAlbumId);
        dto.FileName.ShouldBeEquivalentTo(entity.FileName);
        dto.Description.ShouldBeEquivalentTo(entity.Description);
        dto.SizeInBytes.ShouldBeEquivalentTo(entity.SizeInBytes);
        dto.FileExtension.ShouldBeEquivalentTo(entity.FileExtension);
        dto.CreatedBy.ShouldBeEquivalentTo(entity.CreatedBy);
        dto.Created.ShouldBeEquivalentTo(entity.Created);
        dto.LastModifiedBy.ShouldBeEquivalentTo(entity.LastModifiedBy);
        dto.LastModified.ShouldBeEquivalentTo(entity.LastModified);
        dto.Active.ShouldBeEquivalentTo(entity.Active);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToDto_NullMediaEntity_ThrowsArgumentNullException()
    {
        // arrange
        Media? entity = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entity!.ToDto());
    }

    [Fact]
    public void ToDto_MappingTagEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new Tag
        {
            Id = Guid.NewGuid(),
            Name = "SampleTag"
        };
        
        // act
        var dto = entity.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.Name.ShouldBeEquivalentTo(entity.Name);
    }

    [Fact]
    public void ToDto_NullTagEntity_ThrowsArgumentNullException()
    {
        // arrange
        Tag? entity = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entity!.ToDto());
    }

    [Fact]
    public void ToDto_MappingKnowledgeEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new Knowledge
        {
            Id = Guid.NewGuid(),
            Title = "Sample Knowledge",
            Quote = "This is some sample knowledge content."
        };
        
        // act
        var dto = entity.ToDto();
        
        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.Title.ShouldBeEquivalentTo(entity.Title);
        dto.Quote.ShouldBeEquivalentTo(entity.Quote);
    }

    [Fact]
    public void ToDto_NullKnowledgeEntity_ThrowsArgumentNullException()
    {
        // arrange
        Knowledge? entity = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entity!.ToDto());
    }
}