namespace Tests.Unit.Api.Extensions.MapperExtensionsTests;

public class ToGetModels
{
    [Fact]
    public void ToModels_FromMediaAlbumDtos_MapsAllPropertiesCorrectly()
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
        var models = dtos.ToGetModels().ToList();

        // assert
        models.Count.ShouldBe(2);
        models[0].Id.ShouldBeEquivalentTo(dtos[0].Id);
        models[0].Name.ShouldBeEquivalentTo(dtos[0].Name);
        models[1].Id.ShouldBeEquivalentTo(dtos[1].Id);
        models[1].Name.ShouldBeEquivalentTo(dtos[1].Name);
    }

    [Fact]
    public void ToModels_FromNullMediaAlbumDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbumDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToGetModels());
    }

    [Fact]
    public void ToModels_FromEmptyMediaAlbumDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.ShouldBeEmpty();
    }

    [Fact]
    public void ToModels_FromMediaDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<MediaDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FileName = "image1.jpg",
                Description = "A sample image",
                SizeInBytes = 12345,
                FileExtension = ".jpg", Created = DateTime.UtcNow,
                Tags = new List<TagDto>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tag1",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tag2",
                        Created = DateTime.UtcNow
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                FileName = "image2.jpg",
                Description = "Another sample image",
                SizeInBytes = 67890,
                FileExtension = ".jpg",
                Created = DateTime.UtcNow,
                Tags = new List<TagDto>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tag3",
                        Created = DateTime.UtcNow
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tag4",
                        Created = DateTime.UtcNow
                    }
                }
            }
        };

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.Count.ShouldBe(dtos.Count);
        for (var i = 0; i < models.Count; i++)
        {
            models[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            models[i].FileName.ShouldBeEquivalentTo(dtos[i].FileName);
            models[i].Description.ShouldBeEquivalentTo(dtos[i].Description);
            models[i].SizeInBytes.ShouldBeEquivalentTo(dtos[i].SizeInBytes);
            models[i].Tags.Count().ShouldBe(dtos[i].Tags.Count);
        }
    }

    [Fact]
    public void ToModels_FromNullMediaDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToGetModels());
    }

    [Fact]
    public void ToModels_FromEmptyMediaDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaDto>();

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.ShouldBeEmpty();
    }

    [Fact]
    public void ToModels_FromTagDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<TagDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tag1",
                Created = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tag2",
                Created = DateTime.UtcNow
            }
        };

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.Count.ShouldBe(dtos.Count);
        for (var i = 0; i < models.Count; i++)
        {
            models[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            models[i].Name.ShouldBeEquivalentTo(dtos[i].Name);
        }
    }

    [Fact]
    public void ToModels_FromNullTagDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<TagDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToGetModels());
    }

    [Fact]
    public void ToModels_FromEmptyTagDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<TagDto>();

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.ShouldBeEmpty();
    }

    [Fact]
    public void ToModels_FromKnowledgeDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<KnowledgeDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge 1",
                Quote = "Content for knowledge 1",
                Created = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge 2",
                Quote = "Content for knowledge 2",
                Created = DateTime.UtcNow
            }
        };

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.Count.ShouldBe(dtos.Count);
        for (var i = 0; i < models.Count; i++)
        {
            models[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            models[i].Title.ShouldBeEquivalentTo(dtos[i].Title);
            models[i].Quote.ShouldBeEquivalentTo(dtos[i].Quote);
        }
    }

    [Fact]
    public void ToModels_FromNullKnowledgeDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<KnowledgeDto>? dtos = null;

        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToGetModels());
    }

    [Fact]
    public void ToModels_FromEmptyKnowledgeDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<KnowledgeDto>();

        // act
        var models = dtos.ToGetModels().ToList();

        // assert
        models.ShouldBeEmpty();
    }
}
