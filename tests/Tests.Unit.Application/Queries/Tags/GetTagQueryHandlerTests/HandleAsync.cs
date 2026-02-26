using MaaldoCom.Api.Application.Queries.Tags;

namespace Tests.Unit.Application.Queries.Tags.GetTagQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_ByIdIsValid_ReturnsTag()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tag = new TagDto()
        {
            Id = Guid.NewGuid(),
            Name = "name1",
        };

        var query = new GetTagQuery(tag.Id);
        var handler = new GetTagQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetTagDetailAsync(tag.Id, ct)).Returns(tag);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(tag);
    }

    [Fact]
    public async Task HandleAsync_ByIdNotValid_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var query = new GetTagQuery(Guid.NewGuid());
        var handler = new GetTagQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.GetTagDetailAsync(query.Id!.Value, ct)).Returns(default(TagDto));

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Tag");
    }

    [Fact]
    public async Task HandleAsync_ByUrlFriendlyNameNotInCachedList_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tag = new TagDto()
        {
            Id = Guid.NewGuid(),
            Name = "name1",
        };

        var query = new GetTagQuery(tag.Name!);
        var handler = new GetTagQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListTagsAsync(ct)).Returns(new List<TagDto> { new(), new() });

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Tag");
        A.CallTo(() => cacheManager.GetTagDetailAsync(A.Dummy<Guid>(), ct)).MustNotHaveHappened();
    }

    [Fact]
    public async Task HandleAsync_ByUrlFriendlyNameInCachedListAndInDb_ReturnsTag()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tag = new TagDto()
        {
            Id = Guid.NewGuid(),
            Name = "name1",
        };

        var query = new GetTagQuery(tag.Name!);
        var handler = new GetTagQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListTagsAsync(ct)).Returns(new List<TagDto> { new(), tag, new() });
        A.CallTo(() => cacheManager.GetTagDetailAsync(tag.Id, ct)).Returns(tag);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(tag);
    }

    [Fact]
    public async Task HandleAsync_ByUrlFriendlyNameInCachedListButNotInDb_ReturnsNotFound()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tag = new TagDto()
        {
            Id = Guid.NewGuid(),
            Name = "name1",
        };

        var query = new GetTagQuery(tag.Name!);
        var handler = new GetTagQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListTagsAsync(ct)).Returns(new List<TagDto> { new(), tag, new() });
        A.CallTo(() => cacheManager.GetTagDetailAsync(tag.Id, ct)).Returns(default(TagDto));

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].ShouldBeOfType<EntityNotFoundError>();
        result.Errors[0].Metadata["EntityType"].ShouldBe("Tag");
    }
}
