using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.ListMediaAlbumsQueryHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task Given_ExecuteAsync_WhenInvoked_ThenAlwaysReturnsMediaAlbums()
    {
        // arrange
        var query = new ListMediaAlbumsQuery();
        var handler = new ListMediaAlbumsQueryHandler();

        // act
        var result = await handler.ExecuteAsync(query, CancellationToken.None);

        // assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}