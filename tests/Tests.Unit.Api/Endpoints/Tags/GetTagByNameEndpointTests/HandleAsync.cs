namespace Tests.Unit.Api.Endpoints.Tags.GetTagByNameEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidName_ReturnsTags()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetTagQuery, TagDto>>();
        var endpoint = Factory.Create<GetTagByNameEndpoint>(handler);
        const string name = "test-album";
        var result = new Result<TagDto>().WithValue(new TagDto { Name = name });

        A.CallTo(() => handler.HandleAsync(A<GetTagQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetTagByNameRequest { Name = name }, CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Name.ShouldBe(name);
        response.ShouldBeOfType<GetTagDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidName_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetTagQuery, TagDto>>();
        var endpoint = Factory.Create<GetTagByNameEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetTagQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetTagByNameRequest(), CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
