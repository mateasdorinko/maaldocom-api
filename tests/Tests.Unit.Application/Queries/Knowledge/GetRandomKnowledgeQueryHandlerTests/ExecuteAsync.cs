using MaaldoCom.Services.Application.Queries.Knowledge;

namespace Tests.Unit.Application.Queries.Knowledge.GetRandomKnowledgeQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task ExecuteAsync_Invoked_ReturnsRandomizedKnowledge()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var knowledgeList = new List<KnowledgeDto>
        {
            new() { Id = Guid.NewGuid(), Title = "title1", Quote = "quote1" },
            new() { Id = Guid.NewGuid(), Title = "title2", Quote =  "quote2" },
            new() { Id = Guid.NewGuid(), Title = "title3", Quote =  "quote3" }
        };

        var query = new GetRandomKnowledgeQuery();
        var handler = new GetRandomKnowledgeQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListKnowledgeAsync(ct)).Returns(knowledgeList);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        var matchedKnowledge = knowledgeList.FirstOrDefault(k => k.Id == result.Value.Id);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(matchedKnowledge);
    }
}
