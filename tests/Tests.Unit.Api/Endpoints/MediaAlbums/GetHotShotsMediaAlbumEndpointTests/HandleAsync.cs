namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetHotShotsMediaAlbumEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsHotShotsMediaAlbum()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetHotshotsMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetHotShotsMediaAlbumEndpoint>(handler);
        var result = new Result<MediaAlbumDto>().WithValue(A.Dummy<MediaAlbumDto>());

        A.CallTo(() => handler.HandleAsync(A<GetHotshotsMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<GetMediaAlbumDetailResponse>();
    }
}
