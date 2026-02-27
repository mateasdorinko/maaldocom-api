namespace Tests.Unit.Api.Endpoints.Tags.ListTagsEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsTagList()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListTagsQuery, IEnumerable<TagDto>>>();
        var endpoint = Factory.Create<ListTagsEndpoint>(handler);
        var result = new Result<IEnumerable<TagDto>>().WithValue(new List<TagDto>());

        A.CallTo(() => handler.HandleAsync(A<ListTagsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<List<GetTagResponse>>();
    }
}
