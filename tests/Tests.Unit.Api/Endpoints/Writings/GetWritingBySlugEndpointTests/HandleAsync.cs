namespace Tests.Unit.Api.Endpoints.Writings.GetWritingBySlugEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidSlug_ReturnsWriting()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetWritingDetailQuery, WritingDto>>();
        var endpoint = Factory.Create<GetWritingBySlugEndpoint>(handler);
        const string slug = "test-writing";
        var result = new Result<WritingDto>().WithValue(new WritingDto { Slug = slug });

        A.CallTo(() => handler.HandleAsync(A<GetWritingDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetWritingBySlugRequest { Slug = slug }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Slug.ShouldBe(slug);
        response.ShouldBeOfType<GetWritingDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidSlug_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetWritingDetailQuery, WritingDto>>();
        var endpoint = Factory.Create<GetWritingBySlugEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetWritingDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetWritingBySlugRequest { Slug = "non-existent-slug" }, TestContext.Current.CancellationToken);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
