namespace Tests.Unit.Api.Endpoints.Tags.GetTagByIdEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidId_ReturnsTags()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetTagQuery, TagDto>>();
        var endpoint = Factory.Create<GetTagByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = new Result<TagDto>().WithValue(new TagDto() { Id = id });

        A.CallTo(() => handler.HandleAsync(A<GetTagQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetTagByIdRequest { Id = id }, CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Id.ShouldBe(id);
        response.ShouldBeOfType<GetTagDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidId_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetTagQuery, TagDto>>();
        var endpoint = Factory.Create<GetTagByIdEndpoint>(handler);
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetTagQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetTagByIdRequest(), CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
