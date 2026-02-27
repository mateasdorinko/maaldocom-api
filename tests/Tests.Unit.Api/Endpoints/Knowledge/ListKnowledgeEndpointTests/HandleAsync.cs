namespace Tests.Unit.Api.Endpoints.Knowledge.ListKnowledgeEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsKnowledgeAndHttpOk()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>>>();
        var endpoint = Factory.Create<ListKnowledgeEndpoint>(handler);
        var result = new Result<IEnumerable<KnowledgeDto>>().WithValue(new List<KnowledgeDto>());

        A.CallTo(() => handler.HandleAsync(A<ListKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<List<GetKnowledgeResponse>>();
    }
}
