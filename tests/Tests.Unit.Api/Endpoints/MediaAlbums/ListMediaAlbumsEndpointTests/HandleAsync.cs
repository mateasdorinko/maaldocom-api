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

    [Fact]
    public async Task HandleAsync_WithInactiveAlbums_ExcludesInactiveAlbums()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>>();
        var endpoint = Factory.Create<ListMediaAlbumsEndpoint>(handler);
        var activeAlbum = new MediaAlbumDto { Id = Guid.NewGuid(), Name = "Active Album", UrlFriendlyName = "active", Active = true };
        var inactiveAlbum = new MediaAlbumDto { Id = Guid.NewGuid(), Name = "Inactive Album", UrlFriendlyName = "inactive", Active = false };
        var result = new Result<IEnumerable<MediaAlbumDto>>().WithValue([activeAlbum, inactiveAlbum]);

        A.CallTo(() => handler.HandleAsync(A<ListMediaAlbumsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response as List<GetMediaAlbumResponse>;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response!.Count.ShouldBe(1);
        response[0].Name.ShouldBe(activeAlbum.Name);
    }

    [Fact]
    public async Task HandleAsync_WithHotshotsAlbum_ExcludesHotshotsAlbum()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListMediaAlbumsQuery, IEnumerable<MediaAlbumDto>>>();
        var endpoint = Factory.Create<ListMediaAlbumsEndpoint>(handler);
        var regularAlbum = new MediaAlbumDto { Id = Guid.NewGuid(), Name = "Regular Album", UrlFriendlyName = "regular", Active = true };
        var hotshotsAlbum = new MediaAlbumDto { Id = Guid.NewGuid(), Name = "Hotshots", UrlFriendlyName = "hotshots", Active = true };
        var result = new Result<IEnumerable<MediaAlbumDto>>().WithValue([regularAlbum, hotshotsAlbum]);

        A.CallTo(() => handler.HandleAsync(A<ListMediaAlbumsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response as List<GetMediaAlbumResponse>;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response!.Count.ShouldBe(1);
        response[0].Name.ShouldBe(regularAlbum.Name);
    }
}
