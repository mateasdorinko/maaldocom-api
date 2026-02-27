namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetMediaByIdEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidIdAndMediaTypeIsOriginal_ReturnsPicDownload()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var stream = A.Fake<Stream>();
        var result = new Result<MediaDto>().WithValue(
            new MediaDto
            {
                Stream = A.Fake<Stream>(),
                FileName = "test-file.jpg",
                SizeInBytes = 1234,
                ContentType = "image/jpeg"
            });

        A.CallTo(() => stream.Length).Returns(result.Value.SizeInBytes);
        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "original" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        var headers = httpResponse.Headers;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        headers.ContentDisposition.ShouldContain($"attachment; filename={result.Value.FileName}; filename*=UTF-8''{result.Value.FileName}");
        headers.ContentLength.ShouldBe(result.Value.SizeInBytes);
        headers.ContentType.ShouldContain(result.Value.ContentType);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithValidIdAndMediaTypeIsOriginal_ReturnsVidDownload()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var stream = A.Fake<Stream>();
        var result = new Result<MediaDto>().WithValue(
            new MediaDto
            {
                Stream = A.Fake<Stream>(),
                FileName = "test-file.mp4",
                SizeInBytes = 1234,
                ContentType = "video/mp4"
            });

        A.CallTo(() => stream.Length).Returns(result.Value.SizeInBytes);
        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "original" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        var headers = httpResponse.Headers;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        headers.ContentDisposition.ShouldContain($"attachment; filename={result.Value.FileName}; filename*=UTF-8''{result.Value.FileName}");
        headers.ContentLength.ShouldBe(result.Value.SizeInBytes);
        headers.ContentType.ShouldContain(result.Value.ContentType);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidIdAndMediaTypeIsOriginal_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "original" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithValidIdAndMediaTypeIsViewer_ReturnsViewerMedia()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var stream = A.Fake<Stream>();
        var result = new Result<MediaDto>().WithValue(
            new MediaDto
            {
                Stream = A.Fake<Stream>(),
                FileName = "test-file.jpg",
                SizeInBytes = 1234,
                ContentType = "image/jpeg"
            });

        A.CallTo(() => stream.Length).Returns(result.Value.SizeInBytes);
        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "viewer" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        var headers = httpResponse.Headers;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        headers.ContentDisposition.ShouldBeEmpty();
        headers.ContentLength.ShouldBe(result.Value.SizeInBytes);
        headers.ContentType.ShouldContain(result.Value.ContentType);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidIdAndMediaTypeIsViewer_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "viewer" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithValidIdAndMediaTypeIsThumb_ReturnsThumbMedia()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var stream = A.Fake<Stream>();
        var result = new Result<MediaDto>().WithValue(
            new MediaDto
            {
                Stream = A.Fake<Stream>(),
                FileName = "test-file.jpg",
                SizeInBytes = 1234,
                ContentType = "image/jpeg"
            });

        A.CallTo(() => stream.Length).Returns(result.Value.SizeInBytes);
        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "thumb" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        var headers = httpResponse.Headers;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        headers.ContentDisposition.ShouldBeEmpty();
        headers.ContentLength.ShouldBe(result.Value.SizeInBytes);
        headers.ContentType.ShouldContain(result.Value.ContentType);
        response.ShouldBeOfType<GetMediaResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidIdAndMediaTypeIsThumb_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaBlobQuery, MediaDto>>();
        var endpoint = Factory.Create<GetMediaByIdEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetMediaBlobQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetMediaByIdRequest { MediaType = "thumb" }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        var httpResponse = endpoint.HttpContext.Response;
        httpResponse.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        response.ShouldBeOfType<GetMediaResponse>();
    }
}
