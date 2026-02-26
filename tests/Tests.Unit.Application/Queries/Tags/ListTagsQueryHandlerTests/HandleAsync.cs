using MaaldoCom.Services.Application.Queries.Tags;

namespace Tests.Unit.Application.Queries.Tags.ListTagsQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsTagList()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tagList = new List<TagDto>
        {
            new() { Id = Guid.NewGuid(), Name = "tag1" },
            new() { Id = Guid.NewGuid(), Name = "tag2" },
            new() { Id = Guid.NewGuid(), Name = "tag3" }
        };

        var query = new ListTagsQuery();
        var handler = new ListTagsQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListTagsAsync(ct)).Returns(tagList);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(tagList);
    }
}
