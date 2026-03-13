using MaaldoCom.Api.Application.Queries.Writings;

namespace Tests.Unit.Application.Queries.Writings.ListWritingsQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsActiveWritingList()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var writings = new List<WritingDto>
        {
            new() { Id = Guid.NewGuid(), Title = "writing 1", Slug =  "writing-1", Active =  true },
            new() { Id = Guid.NewGuid(), Title = "writing 2", Slug =  "writing-2", Active =  false  },
            new() { Id = Guid.NewGuid(), Title = "writing 3", Slug =  "writing-3", Active =  true  },
            new() { Id = Guid.NewGuid(), Title = "writing 4", Slug =  "writing-4", Active =  false  },
        };

        var query = new ListWritingsQuery();
        var handler = new ListWritingsQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListWritingsAsync(ct)).Returns(writings);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.Count().ShouldBe(2);
    }
}
