namespace Tests.Unit.Api.Endpoints.MapperExtensionsTests;

public class ToModel
{
    [Fact]
    public void ToModel_MappingMediaAlbumDto_MapsAllPropertiesCorrectly()
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
        model.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToModel_NullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModels_MappingMediaAlbumDtos_MapsAllPropertiesCorrectly()
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
        var models = dtos.ToModels().ToList();

        // assert
        models.Count.ShouldBe(2);
        models[0].Id.ShouldBeEquivalentTo(dtos[0].Id);
        models[0].Name.ShouldBeEquivalentTo(dtos[0].Name);
        models[1].Id.ShouldBeEquivalentTo(dtos[1].Id);
        models[1].Name.ShouldBeEquivalentTo(dtos[1].Name);
    }
    
    [Fact]
    public void ToModels_NullMediaAlbumDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbumDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToModels());
    }

    [Fact]
    public void ToDetailModel_MappingMediaAlbumDto_MapsAllPropertiesCorrectly()
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
        model.Tags.First().Name.ShouldBeEquivalentTo("SampleTag");
    }

    [Fact]
    public void ToDetailModel_NullMediaAlbumDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaAlbumDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToDetailModel());
    }
    
    [Fact]
    public void ToDetailModels_MappingMediaAlbumDtos_MapsAllPropertiesCorrectly()
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
    public void ToDetailModels_NullMediaAlbumDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbumDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToDetailModels());
    }

    [Fact]
    public void ToModel_MappingMediaDto_MapsAllPropertiesCorrectly()
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
    public void ToModel_NullMediaDto_ThrowsArgumentNullException()
    {
        // arrange
        MediaDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModel_MappingTagDto_MapsAllPropertiesCorrectly()
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
    public void ToModel_NullTagDto_ThrowsArgumentNullException()
    {
        // arrange
        TagDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }

    [Fact]
    public void ToModel_MappingKnowledgeDto_MapsAllPropertiesCorrectly()
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
    public void ToModel_NullKnowledgeDto_ThrowsArgumentNullException()
    {
        // arrange
        KnowledgeDto? dto = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dto!.ToModel());
    }
}