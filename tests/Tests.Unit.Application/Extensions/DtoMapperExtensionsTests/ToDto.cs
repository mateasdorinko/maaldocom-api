namespace Tests.Unit.Application.Extensions.DtoMapperExtensionsTests;

public class ToDto
{
    [Fact]
    public void ToDto_FromMediaAlbumEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new MediaAlbum
        {
            Id = Guid.NewGuid(),
            Name = "Sample Album",
            Slug = "sample-album",
            Description = "This is a sample media album.",
            CreatedBy = "tester",
            Created = DateTime.UtcNow,
            LastModifiedBy = "tester",
            LastModified = DateTime.UtcNow,
            Active = true,
            MediaAlbumTags = new List<MediaAlbumTag>
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
        dto.Slug.ShouldBeEquivalentTo(entity.Slug);
        dto.Description.ShouldBeEquivalentTo(entity.Description);
        dto.CreatedBy.ShouldBeEquivalentTo(entity.CreatedBy);
        dto.Created.ShouldBeEquivalentTo(entity.Created);
        dto.LastModifiedBy.ShouldBeEquivalentTo(entity.LastModifiedBy);
        dto.LastModified.ShouldBeEquivalentTo(entity.LastModified);
        dto.Active.ShouldBeEquivalentTo(entity.Active);
        dto.Tags.Count.ShouldBe(1);
        dto.Tags[0].Name.ShouldBeEquivalentTo("SampleTag");
        dto.Media.Count.ShouldBe(1);
        dto.Media[0].FileName.ShouldBeEquivalentTo("sample.jpg");
    }

    [Fact]
    public void ToDto_FromMediaAlbumEntityWithNullMedia_DefaultMediaIdIsGuidEmpty()
    {
        // arrange
        var entity = new MediaAlbum(); // Media is null by default

        // act
        var dto = entity.ToDto();

        // assert
        dto.DefaultMediaId.ShouldBe(Guid.Empty);
    }

    [Fact]
    public void ToDto_FromMediaAlbumEntityWithEmptyMedia_DefaultMediaIdIsGuidEmpty()
    {
        // arrange
        var entity = new MediaAlbum { Media = new List<Media>() };

        // act
        var dto = entity.ToDto();

        // assert
        dto.DefaultMediaId.ShouldBe(Guid.Empty);
    }

    [Fact]
    public void ToDto_FromMediaEntity_MapsAllPropertiesCorrectly()
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
            MediaTags = new List<MediaTag>
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
        dto.Tags[0].Name.ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToDto_FromWritingEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new Writing
        {
            Id = Guid.NewGuid(),
            Blurb = "This is a sample blurb.",
            Body = "This is the body of the writing.",
            Title = "Sample Writing",
            Slug = "sample-writing",
            CreatedBy = "tester",
            Created = DateTime.UtcNow,
            LastModifiedBy = "tester",
            LastModified = DateTime.UtcNow,
            Active = true
        };

        // act
        var dto = entity.ToDto();

        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.Blurb.ShouldBeEquivalentTo(entity.Blurb);
        dto.Body.ShouldBeEquivalentTo(entity.Body);
        dto.Title.ShouldBeEquivalentTo(entity.Title);
        dto.Slug.ShouldBeEquivalentTo(entity.Slug);
        dto.CreatedBy.ShouldBeEquivalentTo(entity.CreatedBy);
        dto.Created.ShouldBeEquivalentTo(entity.Created);
        dto.LastModifiedBy.ShouldBeEquivalentTo(entity.LastModifiedBy);
        dto.LastModified.ShouldBeEquivalentTo(entity.LastModified);
        dto.Active.ShouldBeEquivalentTo(entity.Active);
    }

    [Fact]
    public void ToDto_FromTagEntity_MapsAllPropertiesCorrectly()
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
    public void ToDto_FromCommentEntity_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entity = new Comment
        {
            Id = Guid.NewGuid(),
            Author = "Commenter",
            Body = "This is a sample comment.",
            Created = DateTime.UtcNow
        };

        // act
        var dto = entity.ToDto();

        // assert
        dto.Id.ShouldBeEquivalentTo(entity.Id);
        dto.Author.ShouldBeEquivalentTo(entity.Author);
        dto.Body.ShouldBeEquivalentTo(entity.Body);
        dto.CreatedBy.ShouldBeEquivalentTo(entity.CreatedBy);
        dto.Created.ShouldBeEquivalentTo(entity.Created);
        dto.LastModifiedBy.ShouldBeEquivalentTo(entity.LastModifiedBy);
        dto.LastModified.ShouldBeEquivalentTo(entity.LastModified);
        dto.Active.ShouldBeEquivalentTo(entity.Active);
    }

    [Fact]
    public void ToDto_FromKnowledgeEntity_MapsAllPropertiesCorrectly()
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
}
