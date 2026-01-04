using MaaldoCom.Services.Application.Queries.Knowledge;

namespace Tests.Unit.Application.Queries.Knowledge.GetKnowledgeQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task ExecuteAsync_InvokedWithExistentId_ReturnsKnowledge()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var id = Guid.NewGuid();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetKnowledgeQuery(user, id);
        var handler = new GetKnowledgeQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListKnowledgeAsync(ct))
            .Returns([
                new KnowledgeDto { Id = Guid.NewGuid(), Title = "title1", Quote = "quote1" },
                new KnowledgeDto { Id = id, Title = "title2", Quote =  "quote2" },
                new KnowledgeDto { Id = Guid.NewGuid(), Title = "title3", Quote =  "quote3" }
            ]);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.Id.ShouldBe(id);
        result.Value.Title.ShouldBe("title2");
        result.Value.Quote.ShouldBe("quote2");
    }

    [Fact]
    public async Task ExecuteAsync_InvokedWithNonExistentId_ReturnsNotFound()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetKnowledgeQuery(user, Guid.NewGuid());
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
        result.Errors[0].ShouldBeOfType<EntityNotFound>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Knowledge");
    }
}