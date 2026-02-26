using MaaldoCom.Services.Api.Endpoints.Knowledge;
using MaaldoCom.Services.Application.Queries.Knowledge;

namespace Tests.Unit.Api.Endpoints.Knowledge.ListKnowledgeEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsKnowledgeAndHttpOk()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>>>();
        var endpoint = Factory.Create<ListKnowledgeEndpoint>(handler);
        var result = new Result<IEnumerable<KnowledgeDto>>()
            .WithValue(new List<KnowledgeDto> { new() { Id = Guid.NewGuid(), Title = "Title1", Quote = "Quote1" } });

        A.CallTo(() => handler.HandleAsync(A<ListKnowledgeQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe(200);
        response.ShouldNotBeNull();
    }
}
