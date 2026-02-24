using MaaldoCom.Services.Api.Endpoints.Knowledge;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace Tests.Unit.Api.Endpoints.Knowledge.ListKnowledgeEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsKnowledgeAndHttpOk()
    {
        // arrange
        var endpoint = Factory.Create<ListKnowledgeEndpoint>();
        var handler = A.Fake<ICommandHandler<ListKnowledgeQuery, Result<IEnumerable<KnowledgeDto>>>>();
        var knowledge = new List<KnowledgeDto> { new() { Id = Guid.NewGuid(), Title = "Title1", Quote = "Quote1" } };
        var result = new Result<IEnumerable<KnowledgeDto>>().WithValue(knowledge);

        var query = new ListKnowledgeQuery();

        A.CallTo(() => handler.ExecuteAsync(A<ListKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        handler.RegisterForTesting();

        await query.ExecuteAsync();

        // act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe(200);
        response.ShouldNotBeNull();
    }
}
