namespace Tests.Unit.Application.Extensions.EntityMapperExtensionsTests;

public class ToEntities
{
    [Fact]
    public void ToEntities_FromMediaAlbumDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var dtos = new List<MediaAlbumDto>
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
        var entities = dtos.ToEntities(user).ToList();

        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Name.ShouldBeEquivalentTo(dtos[i].Name);
            entities[i].Slug.ShouldBeEquivalentTo(dtos[i].Slug);
            entities[i].Description.ShouldBeEquivalentTo(dtos[i].Description);
        }
    }

    [Fact]
    public void ToEntities_FromEmptyMediaAlbumDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaAlbumDto>();
        var user = A.Fake<ClaimsPrincipal>();

        // act
        var entities = dtos.ToEntities(user);

        // assert
        entities.ShouldBeEmpty();
    }

    [Fact]
    public void ToEntities_FromMediaDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<MediaDto>
        {
            new() { Id = Guid.NewGuid(), FileName = "file1.jpg", Description = "Description for file 1." },
            new() { Id = Guid.NewGuid(), FileName = "file2.jpg", Description = "Description for file 2." }
        };
        var user = A.Fake<ClaimsPrincipal>();

        // act
        var entities = dtos.ToEntities(user).ToList();

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
    public void ToEntities_FromEmptyMediaDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<MediaDto>();
        var user = A.Fake<ClaimsPrincipal>();

        // act
        var entities = dtos.ToEntities(user);

        // assert
        entities.ShouldBeEmpty();
    }

    [Fact]
    public void ToEntities_FromWritingDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<WritingDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Writing 1",
                Blurb = "Blurb for writing 1.",
                Body = "Body for Writing 1.",
                Slug = "writing-1"
            },
            new() {
                Id = Guid.NewGuid(),
                Title = "Writing 2",
                Blurb = "Blurb for writing 2.",
                Body = "Body for Writing 2.",
                Slug = "writing-2"
            }
        };
        var user = A.Fake<ClaimsPrincipal>();

        // act
        var entities = dtos.ToEntities(user).ToList();

        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Title.ShouldBeEquivalentTo(dtos[i].Title);
            entities[i].Blurb.ShouldBeEquivalentTo(dtos[i].Blurb);
            entities[i].Body.ShouldBeEquivalentTo(dtos[i].Body);
            entities[i].Slug.ShouldBeEquivalentTo(dtos[i].Slug);
        }
    }

    [Fact]
    public void ToEntities_FromTagDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<TagDto>
        {
            new() { Id = Guid.NewGuid(), Name = "Tag1" }, new() { Id = Guid.NewGuid(), Name = "Tag2" }
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
    public void ToEntities_FromEmptyTagDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<TagDto>();

        // act
        var entities = dtos.ToEntities();

        // assert
        entities.ShouldBeEmpty();
    }

    [Fact]
    public void ToEntities_FromCommentDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<CommentDto>
        {
            new(){ Id = Guid.NewGuid(), Author = "Author1", Body = "Comment body for comment 1." },
            new(){ Id = Guid.NewGuid(), Author = "Author2", Body = "Comment body for comment 2." }
        };

        // act
        var entities = dtos.ToEntities().ToList();

        // assert
        entities.Count.ShouldBeEquivalentTo(dtos.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            entities[i].Id.ShouldBeEquivalentTo(dtos[i].Id);
            entities[i].Author.ShouldBeEquivalentTo(dtos[i].Author);
            entities[i].Body.ShouldBeEquivalentTo(dtos[i].Body);
        }
    }

    [Fact]
    public void ToEntities_FromKnowledgeDtos_MapsAllPropertiesCorrectly()
    {
        // arrange
        var dtos = new List<KnowledgeDto>
        {
            new() { Id = Guid.NewGuid(), Title = "Knowledge1", Quote = "Content for knowledge 1." },
            new() { Id = Guid.NewGuid(), Title = "Knowledge2", Quote = "Content for knowledge 2." }
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
    public void ToEntities_FromEmptyKnowledgeDtos_ReturnsEmptyList()
    {
        // arrange
        var dtos = new List<KnowledgeDto>();

        // act
        var entities = dtos.ToEntities();

        // assert
        entities.ShouldBeEmpty();
    }
}
