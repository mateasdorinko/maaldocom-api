using MaaldoCom.Services.Application.Queries.Tags;

namespace Tests.Unit.Application.Queries.Tags.ListTagsQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task ExecuteAsync_Invoked_ReturnsTagList()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var cacheManager = A.Fake<ICacheManager>();
        var ct = CancellationToken.None;

        var tagList = new List<TagDto>
        {
            new() { Id = Guid.NewGuid(), Name = "tag1" },
            new() { Id = Guid.NewGuid(), Name = "tag2" },
            new() { Id = Guid.NewGuid(), Name = "tag3" }
        };

        var query = new ListTagsQuery(user);
        var handler = new ListTagsQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListTagsAsync(ct)).Returns(tagList);

        // act
        var result = await handler.ExecuteAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(tagList);
    }
}