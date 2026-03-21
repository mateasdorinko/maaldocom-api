namespace Tests.Unit.Application.Extensions.DtoMapperExtensionsTests;

public class ToDtos
{
    [Fact]
    public void ToDtos_FromMediaAlbumEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<MediaAlbum>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Album 1",
                Slug = "album-1",
                Description = "Description for album 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Album 2",
                Slug = "album-2",
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
            dtos[i].Slug.ShouldBeEquivalentTo(entities[i].Slug);
            dtos[i].Description.ShouldBeEquivalentTo(entities[i].Description);
        }
    }

    [Fact]
    public void ToDtos_FromEmptyMediaAlbumEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<MediaAlbum>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_FromMediaEntities_MapsAllPropertiesCorrectly()
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
    public void ToDtos_FromEmptyMediaEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Media>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_FromWritingEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<Writing>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Writing 1",
                Body = "Body for Writing 1.",
                Blurb = "Blurb for Writing 1.",
                Slug = "writing-1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Writing 2",
                Body = "Body for Writing 2.",
                Blurb = "Blurb for Writing 2.",
                Slug = "writing-2"
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
            dtos[i].Body.ShouldBeEquivalentTo(entities[i].Body);
            dtos[i].Blurb.ShouldBeEquivalentTo(entities[i].Blurb);
            dtos[i].Slug.ShouldBeEquivalentTo(entities[i].Slug);
        }
    }

    [Fact]
    public void ToDtos_FromEmptyWritingEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Writing>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_FromTagEntities_MapsAllPropertiesCorrectly()
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
    public void ToDtos_FromEmptyTagEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Tag>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_FromCommentEntities_MapsAllPropertiesCorrectly()
    {
        // arrange
        var entities = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "Author1",
                Body = "Comment body 1."
            },
            new()
            {
                Id = Guid.NewGuid(),
                Author = "Author2",
                Body = "Comment body 2."
            }
        };

        // act
        var dtos = entities.ToDtos().ToList();

        // assert
        dtos.Count.ShouldBeEquivalentTo(entities.Count);
        for (var i = 0; i < entities.Count; i++)
        {
            dtos[i].Id.ShouldBeEquivalentTo(entities[i].Id);
            dtos[i].Author.ShouldBeEquivalentTo(entities[i].Author);
        }
    }

    [Fact]
    public void ToDtos_FromEmptyCommentEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Comment>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }

    [Fact]
    public void ToDtos_FromKnowledgeEntities_MapsAllPropertiesCorrectly()
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
    public void ToDtos_FromEmptyKnowledgeEntities_ReturnsEmptyList()
    {
        // arrange
        var entities = new List<Knowledge>();

        // act
        var dtos = entities.ToDtos();

        // assert
        dtos.ShouldBeEmpty();
    }
}
