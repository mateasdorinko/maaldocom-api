namespace Tests.Unit.Api.Endpoints.System.PostCacheRefreshEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsNoContent()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<CacheRefreshCommand>>();
        var endpoint = Factory.Create<PostCacheRefreshEndpoint>(handler);
        var result = Result.Ok();

        A.CallTo(() => handler.HandleAsync(A<CacheRefreshCommand>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NoContent);
    }
}
