using MaaldoCom.Api.Application.Queries.Knowledge;

namespace Tests.Unit.Application.Queries.Knowledge.ListKnowledgeQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsKnowledgeList()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var knowledgeList = new List<KnowledgeDto>
        {
            new() { Id = Guid.NewGuid(), Title = "title1", Quote = "quote1" },
            new() { Id = Guid.NewGuid(), Title = "title2", Quote =  "quote2" },
            new() { Id = Guid.NewGuid(), Title = "title3", Quote =  "quote3" }
        };

        var query = new ListKnowledgeQuery();
        var handler = new ListKnowledgeQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListKnowledgeAsync(ct)).Returns(knowledgeList);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(knowledgeList);
    }
}
