using Microsoft.EntityFrameworkCore;
using MaaldoCom.Api.Application.Commands.MediaAlbums;
using Tests.Unit.Application.TestHelpers;

namespace Tests.Unit.Application.Commands.MediaAlbums.CreateMediaAlbumCommandHandlerTests;

public class HandleAsync
{
    private static IMaaldoComDbContext CreateDbContext(List<Tag>? existingTags = null)
    {
        var dbContext = A.Fake<IMaaldoComDbContext>();
        A.CallTo(() => dbContext.Tags).Returns(DbSetHelper.CreateFakeDbSet(existingTags ?? []));
        A.CallTo(() => dbContext.MediaAlbums).Returns(A.Fake<DbSet<MediaAlbum>>());
        return dbContext;
    }

    private static CreateMediaAlbumCommand BuildCommand(string albumName = "My Album", params string[] tagNames)
    {
        var tags = tagNames.Select(n => new TagDto { Name = n }).ToList();
        var albumDto = new MediaAlbumDto
        {
            Name = albumName,
            UrlFriendlyName = albumName.ToLower().Replace(" ", "-"),
            Description = "A test album",
            Tags = tags,
            Media =
            [
                new MediaDto
                {
                    FileName = "photo.jpg",
                    FileExtension = ".jpg",
                    Tags = []
                }
            ]
        };
        return new CreateMediaAlbumCommand(new ClaimsPrincipal(), albumDto);
    }

    [Fact]
    public async Task HandleAsync_WithNoExistingTags_CreatesAlbumAndReturnsDto()
    {
        // arrange
        var dbContext = CreateDbContext();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = BuildCommand("Summer 2024", "travel", "sunset");
        var handler = new CreateMediaAlbumCommandHandler(dbContext, cacheManager);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.Name.ShouldBe("Summer 2024");
        A.CallTo(() => dbContext.SaveChangesAsync(command.User, ct, A<bool>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithExistingTags_ReusesTags()
    {
        // arrange
        var existingTags = new List<Tag> { new() { Id = Guid.NewGuid(), Name = "travel" } };
        var dbContext = CreateDbContext(existingTags);
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = BuildCommand("Summer 2024", "travel");
        var handler = new CreateMediaAlbumCommandHandler(dbContext, cacheManager);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => dbContext.Tags.AddRangeAsync(A<IEnumerable<Tag>>._, ct)).MustNotHaveHappened();
    }

    [Fact]
    public async Task HandleAsync_WithNewTags_CreatesNewTags()
    {
        // arrange
        var dbContext = CreateDbContext();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = BuildCommand("Summer 2024", "newTag");
        var handler = new CreateMediaAlbumCommandHandler(dbContext, cacheManager);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => dbContext.Tags.AddRangeAsync(
                A<IEnumerable<Tag>>.That.Matches(tags => tags.Any(t => t.Name == "newtag")), ct))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_Always_SetsAlbumActiveToTrue()
    {
        // arrange
        var dbContext = CreateDbContext();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = BuildCommand("My Album");
        command.MediaAlbum.Active = false;
        var handler = new CreateMediaAlbumCommandHandler(dbContext, cacheManager);

        // act
        await handler.HandleAsync(command, ct);

        // assert
        command.MediaAlbum.Active.ShouldBe(true);
    }

    [Fact]
    public async Task HandleAsync_Always_InvalidatesBothCacheKeys()
    {
        // arrange
        var dbContext = CreateDbContext();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = BuildCommand("My Album");
        var handler = new CreateMediaAlbumCommandHandler(dbContext, cacheManager);

        // act
        await handler.HandleAsync(command, ct);

        // assert
        A.CallTo(() => cacheManager.InvalidateCache(CacheKeys.MediaAlbumList, ct)).MustHaveHappenedOnceExactly();
        A.CallTo(() => cacheManager.InvalidateCache(CacheKeys.TagList, ct)).MustHaveHappenedOnceExactly();
    }
}
