namespace Tests.Unit.Api.Endpoints.MediaAlbums.ListMediaAlbumsEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsMediaAlbumList()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>>();
        var endpoint = Factory.Create<ListMediaAlbumsEndpoint>(handler);
        var result = new Result<IEnumerable<MediaAlbumDto>>().WithValue(new List<MediaAlbumDto>());

        A.CallTo(() => handler.HandleAsync(A<ListMediaAlbumsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<List<GetMediaAlbumResponse>>();
    }
}
