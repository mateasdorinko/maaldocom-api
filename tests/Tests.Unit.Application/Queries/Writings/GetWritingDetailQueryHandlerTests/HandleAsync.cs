using MaaldoCom.Api.Application.Queries.Writings;

namespace Tests.Unit.Application.Queries.Writings.GetWritingDetailQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_ByIdIsValid_ReturnsWriting()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var writings = new List<WritingDto> { new() { Id = Guid.NewGuid(), Title = "title1", } };

        var query = new GetWritingDetailQuery(writings[0].Id);
        var handler = new GetWritingDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListWritingsAsync(ct)).Returns(writings);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(writings[0]);
    }

    [Fact]
    public async Task HandleAsync_ByIdNotValid_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var query = new GetWritingDetailQuery(Guid.NewGuid());
        var handler = new GetWritingDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListWritingsAsync(ct)).Returns(new List<WritingDto>());

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Writing");
    }

    [Fact]
    public async Task HandleAsync_BySlugIsValid_ReturnsWriting()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var writings = new List<WritingDto> { new() { Id = Guid.NewGuid(), Slug = "title1", } };

        var query = new GetWritingDetailQuery(writings[0].Slug!);
        var handler = new GetWritingDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListWritingsAsync(ct)).Returns(writings);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(writings[0]);
    }

    [Fact]
    public async Task HandleAsync_BySlugNotValid_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var query = new GetWritingDetailQuery("non-existing-slug");
        var handler = new GetWritingDetailQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListWritingsAsync(ct)).Returns(new List<WritingDto>());

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Writing");
    }
}
