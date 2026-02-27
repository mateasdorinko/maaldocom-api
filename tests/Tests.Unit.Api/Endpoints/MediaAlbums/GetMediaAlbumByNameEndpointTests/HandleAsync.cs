namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetMediaAlbumByNameEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidName_ReturnsMediaAlbum()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByNameEndpoint>(handler);
        const string name = "test-album";
        var result = new Result<MediaAlbumDto>().WithValue(new MediaAlbumDto { Name = name });

        A.CallTo(() => handler.HandleAsync(A<GetMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaAlbumByNameRequest { Name = name }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Name.ShouldBe(name);
        response.ShouldBeOfType<GetMediaAlbumDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidName_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByNameEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaAlbumByNameRequest(), TestContext.Current.CancellationToken);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
