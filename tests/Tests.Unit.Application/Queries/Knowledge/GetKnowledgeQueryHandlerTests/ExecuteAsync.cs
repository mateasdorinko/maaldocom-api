using MaaldoCom.Services.Application.Queries.Knowledge;

namespace Tests.Unit.Application.Queries.Knowledge.GetKnowledgeQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task ExecuteAsync_InvokedWithExistentId_ReturnsKnowledge()
    {
        // arrange
        var id = Guid.NewGuid();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetKnowledgeQuery(id);
        var handler = new GetKnowledgeQueryHandler(cacheManager);

        var knowledgeList = new List<KnowledgeDto>
        {
            new() { Id = Guid.NewGuid(), Title = "title1", Quote = "quote1" },
            new() { Id = id, Title = "title2", Quote = "quote2" },
            new() { Id = Guid.NewGuid(), Title = "title3", Quote = "quote3" }
        };

        A.CallTo(() => cacheManager.ListKnowledgeAsync(ct)).Returns(knowledgeList);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        var matchedKnowledge = knowledgeList.FirstOrDefault(k => k.Id == result.Value.Id);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(matchedKnowledge);
    }

    [Fact]
    public async Task ExecuteAsync_InvokedWithNonExistentId_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetKnowledgeQuery(Guid.NewGuid());
        var handler = new GetKnowledgeQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListKnowledgeAsync(ct))
            .Returns([
                new KnowledgeDto { Id = Guid.NewGuid(), Title = "title1", Quote = "quote1" },
                new KnowledgeDto { Id = Guid.NewGuid(), Title = "title2", Quote =  "quote2" },
                new KnowledgeDto { Id = Guid.NewGuid(), Title = "title3", Quote =  "quote3" }
            ]);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Knowledge");
    }
}
