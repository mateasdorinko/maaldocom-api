using MaaldoCom.Api.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.ListMediaAlbumsQueryHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsActiveMediaAlbumList()
    {
        // arrange
        var cacheManager = A.Fake<ICacheManager>();
        var ct = TestContext.Current.CancellationToken;

        var mediaAlbums = new List<MediaAlbumDto>
        {
            new() { Id = Guid.NewGuid(), Name = "Album 1", UrlFriendlyName =  "album-1", Active =  true },
            new() { Id = Guid.NewGuid(), Name = "Album 2", UrlFriendlyName =  "album-2", Active =  false  },
            new() { Id = Guid.NewGuid(), Name = "Album 3", UrlFriendlyName =  "album-3", Active =  true  },
            new() { Id = Guid.NewGuid(), Name = "Album 4", UrlFriendlyName =  "album-4", Active =  false  },
        };

        var query = new ListMediaAlbumsQuery();
        var handler = new ListMediaAlbumsQueryHandler(cacheManager);

        A.CallTo(() => cacheManager.ListMediaAlbumsAsync(ct)).Returns(mediaAlbums);

        // act
        var result = await handler.HandleAsync(query, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.Count().ShouldBe(2);
    }
}
