namespace Tests.Unit.Api.Endpoints.Knowledge.GetRandomKnowledgeEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsRandomKnowledge()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetRandomKnowledgeQuery, KnowledgeDto>>();
        var endpoint = Factory.Create<GetRandomKnowledgeEndpoint>(handler);
        var result = new Result<KnowledgeDto>().WithValue(new KnowledgeDto());

        A.CallTo(() => handler.HandleAsync(A<GetRandomKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<GetKnowledgeResponse>();
    }
}
