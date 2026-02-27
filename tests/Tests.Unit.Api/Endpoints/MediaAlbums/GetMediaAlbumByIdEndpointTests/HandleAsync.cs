namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetMediaAlbumByIdEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidId_ReturnsMediaAlbum()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = new Result<MediaAlbumDto>().WithValue(new MediaAlbumDto { Id = id });

        A.CallTo(() => handler.HandleAsync(A<GetMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaAlbumByIdRequest { Id = id }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Id.ShouldBe(id);
        response.ShouldBeOfType<GetMediaAlbumDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidId_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaAlbumByIdRequest { Id = id }, TestContext.Current.CancellationToken);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task HandleAsync_WithMixedActiveMedia_ReturnsOnlyActiveMedia()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var activeMedia = new MediaDto { Id = Guid.NewGuid(), FileName = "active.jpg", Active = true };
        var inactiveMedia = new MediaDto { Id = Guid.NewGuid(), FileName = "inactive.jpg", Active = false };
        var dto = new MediaAlbumDto { Id = id, Media = [activeMedia, inactiveMedia] };
        var result = new Result<MediaAlbumDto>().WithValue(dto);

        A.CallTo(() => handler.HandleAsync(A<GetMediaAlbumDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaAlbumByIdRequest { Id = id }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response!.Media.Count().ShouldBe(1);
        response.Media.First().FileName.ShouldBe(activeMedia.FileName);
    }
}
