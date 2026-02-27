namespace Tests.Unit.Api.Endpoints.MediaAlbums.PostMediaAlbumEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidMediaAlbum_ReturnsCreated()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<CreateMediaAlbumCommand, MediaAlbumDto>>();
        var linkGenerator = A.Fake<LinkGenerator>();
        var id = Guid.NewGuid();
        var result = new Result<MediaAlbumDto>().WithValue(new MediaAlbumDto { Id = id });

        A.CallTo(() => handler.HandleAsync(A<CreateMediaAlbumCommand>.Ignored, A<CancellationToken>.Ignored)).Returns(result);
        A.CallTo(() => linkGenerator.GetPathByAddress(
            A<HttpContext>.Ignored,
            A<string>.Ignored,
            A<RouteValueDictionary>.Ignored,
            A<RouteValueDictionary>.Ignored,
            A<PathString?>.Ignored,
            A<FragmentString>.Ignored,
            A<LinkOptions?>.Ignored))
        .Returns(A.Dummy<string>());

        var endpoint = Factory.Create<PostMediaAlbumEndpoint>(
            ctx => ctx.AddTestServices(s => s.AddSingleton(linkGenerator)),
            handler);

        // act
        await endpoint.HandleAsync(new PostMediaAlbumRequest(), TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.Created);
        endpoint.HttpContext.Response.Headers.ContainsKey("Location").ShouldBeTrue();
        response.Id.ShouldBe(id);
        response.ShouldBeOfType<PostMediaAlbumResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidMediaAlbum_ReturnsBadRequestWithBrokenRules()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<CreateMediaAlbumCommand, MediaAlbumDto>>();
        var endpoint = Factory.Create<PostMediaAlbumEndpoint>(handler);
        var result = Result.Fail(new[] { new Error("Invalid media album data") });

        A.CallTo(() => handler.HandleAsync(A<CreateMediaAlbumCommand>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new PostMediaAlbumRequest(), TestContext.Current.CancellationToken);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
        endpoint.HttpContext.Response.Headers.ContainsKey("Location").ShouldBeFalse();
        endpoint.ValidationFailures.ShouldHaveSingleItem();
        endpoint.ValidationFailures.ShouldContain(f => f.ErrorMessage == result.Errors[0].Message);
    }
}
