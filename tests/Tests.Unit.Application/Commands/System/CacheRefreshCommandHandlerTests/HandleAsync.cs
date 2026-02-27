using MaaldoCom.Api.Application.Commands.System;

namespace Tests.Unit.Application.Commands.System.CacheRefreshCommandHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_CallsRefreshCacheAndReturnsOk()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;
        var command = new CacheRefreshCommand();
        var handler = new CacheRefreshCommandHandler(cacheManager);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => cacheManager.RefreshCacheAsync(ct)).MustHaveHappenedOnceExactly();
    }
}
