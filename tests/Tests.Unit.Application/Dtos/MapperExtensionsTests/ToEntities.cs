namespace Tests.Unit.Application.Dtos.MapperExtensionsTests;

public class ToEntities
{
    [Fact]
    public void ToEntities_MappingMediaAlbumDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Album 1",
                UrlFriendlyName = "album-1",
                Description = "Description for album 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Album 2",
                UrlFriendlyName = "album-2",
                Description = "Description for album 2."
            }
        };
        
        // act
        var entities = dtos.ToEntities().ToList();
        
        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Name.ShouldBeEquivalentTo(dtos[i].Name);
            entities[i].UrlFriendlyName.ShouldBeEquivalentTo(dtos[i].UrlFriendlyName);
            entities[i].Description.ShouldBeEquivalentTo(dtos[i].Description);
        }
    }

    [Fact]
    public void ToEntities_NullMediaAlbumDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbumDto>? dtos = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToEntities());
    }

    [Fact]
    public void ToEntities_EmptyMediaAlbumDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();
        
        // act
        var entities = dtos.ToEntities();
        
        // assert
        entities.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToEntities_MappingMediaDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<MediaDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FileName = "file1.jpg",
                Description = "Description for file 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                FileName = "file2.jpg",
                Description = "Description for file 2."
            }
        };
        
        // act
        var entities = dtos.ToEntities().ToList();
        
        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].FileName.ShouldBeEquivalentTo(dtos[i].FileName);
            entities[i].Description.ShouldBeEquivalentTo(dtos[i].Description);
        }
    }
    
    [Fact]
    public void ToEntities_NullMediaDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaDto>? dtos = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToEntities());
    }

    [Fact]
    public void ToEntities_EmptyMediaDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaDto>();
        
        // act
        var entities = dtos.ToEntities();
        
        // assert
        entities.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToEntities_MappingTagDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<TagDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tag1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Tag2"
            }
        };
        
        // act
        var entities = dtos.ToEntities().ToList();
        
        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Name.ShouldBeEquivalentTo(dtos[i].Name);
        }
    }
    
    [Fact]
    public void ToEntities_NullTagDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<TagDto>? dtos = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToEntities());
    }
    
    [Fact]
    public void ToEntities_EmptyTagDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<TagDto>();
        
        // act
        var entities = dtos.ToEntities();
        
        // assert
        entities.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToEntities_MappingKnowledgeDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<KnowledgeDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge1",
                Quote = "Content for knowledge 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge2",
                Quote = "Content for knowledge 2."
            }
        };
        
        // act
        var entities = dtos.ToEntities().ToList();
        
        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Title.ShouldBeEquivalentTo(dtos[i].Title);
            entities[i].Quote.ShouldBeEquivalentTo(dtos[i].Quote);
        }
    }

    [Fact]
    public void ToEntities_NullKnowledgeDtos_ThrowsArgumentNullException()
    {
        // arrange
        List<KnowledgeDto>? dtos = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => dtos!.ToEntities());
    }
    
    [Fact]
    public void ToEntities_EmptyKnowledgeDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<KnowledgeDto>();
        
        // act
        var entities = dtos.ToEntities();
        
        // assert
        entities.ShouldBeEmpty();
    }
}