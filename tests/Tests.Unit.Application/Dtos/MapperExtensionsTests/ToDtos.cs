namespace Tests.Unit.Application.Dtos.MapperExtensionsTests;

public class ToDtos
{
    [Fact]
    public void ToDtos_MappingMediaAlbumEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<MediaAlbum>
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
        var dtos = entities.ToDtos().ToList();
        
        // assert
        dtos.Count.ShouldBeEquivalentTo(entities.Count);
        for (var i = 0; i < entities.Count; i++)
        {
            dtos[i].Id.ShouldBeEquivalentTo(entities[i].Id);
            dtos[i].Name.ShouldBeEquivalentTo(entities[i].Name);
            dtos[i].UrlFriendlyName.ShouldBeEquivalentTo(entities[i].UrlFriendlyName);
            dtos[i].Description.ShouldBeEquivalentTo(entities[i].Description);
        }
    }

    [Fact]
    public void ToDtos_NullMediaAlbumEntities_ThrowsArgumentNullException()
    {
        // arrange
        List<MediaAlbum>? entities = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entities!.ToDtos());
    }

    [Fact]
    public void ToDtos_EmptyMediaAlbumEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<MediaAlbum>();
        
        // act
        var dtos = entities.ToDtos();
        
        // assert
        dtos.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToDtos_MappingMediaEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<Media>
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
                FileName = "file2.png",
                Description = "Description for file 2."
            }
        };
        
        // act
        var dtos = entities.ToDtos().ToList();
        
        // assert
        dtos.Count.ShouldBeEquivalentTo(entities.Count);
        for (var i = 0; i < entities.Count; i++)
        {
            dtos[i].Id.ShouldBeEquivalentTo(entities[i].Id);
            dtos[i].FileName.ShouldBeEquivalentTo(entities[i].FileName);
            dtos[i].Description.ShouldBeEquivalentTo(entities[i].Description);
        }
    }
    
    [Fact]
    public void ToDtos_NullMediaEntities_ThrowsArgumentNullException()
    {
        // arrange
        List<Media>? entities = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entities!.ToDtos());
    }
    
    [Fact]
    public void ToDtos_EmptyMediaEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Media>();
        
        // act
        var dtos = entities.ToDtos();
        
        // assert
        dtos.ShouldBeEmpty();
    }
    
    [Fact]
    public void ToDtos_MappingTagEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<Tag>
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
        var dtos = entities.ToDtos().ToList();
        
        // assert
        dtos.Count.ShouldBeEquivalentTo(entities.Count);
        for (var i = 0; i < entities.Count; i++)
        {
            dtos[i].Id.ShouldBeEquivalentTo(entities[i].Id);
            dtos[i].Name.ShouldBeEquivalentTo(entities[i].Name);
        }
    }

    [Fact]
    public void ToDtos_NullTagEntities_ThrowsArgumentNullException()
    {
        // arrange
        List<Tag>? entities = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entities!.ToDtos());
    }

    [Fact]
    public void ToDtos_EmptyTagEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Tag>();
        
        // act
        var dtos = entities.ToDtos();
        
        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_MappingKnowledgeEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<Knowledge>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge Item 1",
                Quote = "Content for knowledge item 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Knowledge Item 2",
                Quote = "Content for knowledge item 2."
            }
        };
        
        // act
        var dtos = entities.ToDtos().ToList();
        
        // assert
        dtos.Count.ShouldBeEquivalentTo(entities.Count);
        for (var i = 0; i < entities.Count; i++)
        {
            dtos[i].Id.ShouldBeEquivalentTo(entities[i].Id);
            dtos[i].Title.ShouldBeEquivalentTo(entities[i].Title);
            dtos[i].Quote.ShouldBeEquivalentTo(entities[i].Quote);
        }
    }
    
    [Fact]
    public void ToDtos_NullKnowledgeEntities_ThrowsArgumentNullException()
    {
        // arrange
        List<Knowledge>? entities = null;
        
        // act & assert
        Assert.Throws<ArgumentNullException>(() => entities!.ToDtos());
    }

    [Fact]
    public void ToDtos_EmptyKnowledgeEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Knowledge>();
        
        // act
        var dtos = entities.ToDtos();
        
        // assert
        dtos.ShouldBeEmpty();
    }
}