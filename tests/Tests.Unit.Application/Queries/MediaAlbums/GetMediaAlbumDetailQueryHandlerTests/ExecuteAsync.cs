using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.GetMediaAlbumDetailQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task ExecuteAsync_ByIdIsValid_ReturnsMediaAlbum()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var mediaAlbum = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            Name = "name1",
        };

        var query = new GetMediaAlbumDetailQuery(user, mediaAlbum.Id);
        var handler = new GetMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(mediaAlbum.Id, ct)).Returns(mediaAlbum);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(mediaAlbum);
    }

    [Fact]
    public async Task ExecuteAsync_ByIdNotValid_ReturnsNotFound()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetMediaAlbumDetailQuery(user, Guid.NewGuid());
        var handler = new GetMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(query.Id!.Value, ct)).Returns(default(MediaAlbumDto));

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFound>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("MediaAlbum");
    }

    [Fact]
    public async Task ExecuteAsync_ByUrlFriendlyNameNotInCachedList_ReturnsNotFound()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var mediaAlbum = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            UrlFriendlyName = "name1",
        };

        var query = new GetMediaAlbumDetailQuery(user, mediaAlbum.UrlFriendlyName!);
        var handler = new GetMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListMediaAlbumsAsync(ct)).Returns(new List<MediaAlbumDto> { new(), new() });

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFound>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("MediaAlbum");
        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(A.Dummy<Guid>(), ct)).MustNotHaveHappened();
    }

    [Fact]
    public async Task ExecuteAsync_ByUrlFriendlyNameInCachedListAndInDb_ReturnsMediaAlbum()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var mediaAlbum = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            UrlFriendlyName = "name1",
        };

        var query = new GetMediaAlbumDetailQuery(user, mediaAlbum.UrlFriendlyName!);
        var handler = new GetMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListMediaAlbumsAsync(ct)).Returns(new List<MediaAlbumDto> { new(), mediaAlbum, new() });
        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(mediaAlbum.Id, ct)).Returns(mediaAlbum);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(mediaAlbum);
    }

    [Fact]
    public async Task ExecuteAsync_ByUrlFriendlyNameInCachedListButNotInDb_ReturnsNotFound()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var mediaAlbum = new MediaAlbumDto
        {
            Id = Guid.NewGuid(),
            UrlFriendlyName = "name1",
        };

        var query = new GetMediaAlbumDetailQuery(user, mediaAlbum.UrlFriendlyName!);
        var handler = new GetMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListMediaAlbumsAsync(ct)).Returns(new List<MediaAlbumDto> { new(), mediaAlbum, new() });
        A.CallTo(() => cacheManager.GetMediaAlbumDetailAsync(mediaAlbum.Id, ct)).Returns(default(MediaAlbumDto));

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFound>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("MediaAlbum");
    }
}