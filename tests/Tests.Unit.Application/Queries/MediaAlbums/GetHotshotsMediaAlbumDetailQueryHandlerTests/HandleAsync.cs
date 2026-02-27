using MaaldoCom.Api.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.GetHotshotsMediaAlbumDetailQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WhenCacheReturnsAlbum_ReturnsSuccessWithAlbum()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var mediaAlbum = new MediaAlbumDto { Id = Guid.NewGuid(), Name = "Hotshots" };
        var query = new GetHotshotsMediaAlbumDetailQuery();
        var handler = new GetHotshotsMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetHotshotsMediaAlbumDetailAsync(ct)).Returns(mediaAlbum);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(mediaAlbum);
    }

    [Fact]
    public async Task HandleAsync_WhenCacheReturnsNull_ReturnsSuccessWithNullValue()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var query = new GetHotshotsMediaAlbumDetailQuery();
        var handler = new GetHotshotsMediaAlbumDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetHotshotsMediaAlbumDetailAsync(ct)).Returns(default(MediaAlbumDto));

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBeNull();
    }
}
