namespace Tests.Unit.Api.Endpoints.Knowledge.GetKnowledgeByIdEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidId_ReturnsKnowledge()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetKnowledgeQuery, KnowledgeDto>>();
        var endpoint = Factory.Create<GetKnowledgeByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = new Result<KnowledgeDto>().WithValue(new KnowledgeDto { Id = id });

        A.CallTo(() => handler.HandleAsync(A<GetKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetKnowledgeByIdRequest { Id = id }, CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Id.ShouldBe(id);
        response.ShouldBeOfType<GetKnowledgeResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidId_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetKnowledgeQuery, KnowledgeDto>>();
        var endpoint = Factory.Create<GetKnowledgeByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetKnowledgeByIdRequest { Id = id }, CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
