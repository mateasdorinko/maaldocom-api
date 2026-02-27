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

    [Fact]
    public async Task HandleAsync_WithMixedActiveMedia_ReturnsOnlyActiveMedia()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetHotshotsMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetHotShotsMediaAlbumEndpoint>(handler);
        var activeMedia = new MediaDto { Id = Guid.NewGuid(), FileName = "active.jpg", Active = true };
        var inactiveMedia = new MediaDto { Id = Guid.NewGuid(), FileName = "inactive.jpg", Active = false };
        var dto = new MediaAlbumDto { Media = [activeMedia, inactiveMedia] };
        var result = new Result<MediaAlbumDto>().WithValue(dto);

        A.CallTo(() => handler.HandleAsync(A<GetHotshotsMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response!.Media.Count().ShouldBe(1);
        response.Media.First().FileName.ShouldBe(activeMedia.FileName);
    }
}
