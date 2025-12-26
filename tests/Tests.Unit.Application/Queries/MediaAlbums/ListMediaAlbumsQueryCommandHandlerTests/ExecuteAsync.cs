using MaaldoCom.Services.Application.Queries.MediaAlbums;

namespace Tests.Unit.Application.Queries.MediaAlbums.ListMediaAlbumsQueryCommandHandlerTests;

public class ExecuteAsync : BaseTest
{
    [Fact]
    public async Task Given_ExecuteAsync_WhenInvoked_ThenAlwaysReturnsMediaAlbums()
    {
        // arrange
        var query = new ListMediaAlbumsQueryCommand(MockingHelper.User.FakedObject);
        var handler = new ListMediaAlbumsQueryCommandHandler(MockingHelper.MaaldoComDbContext.FakedObject, MockingHelper.HybridCache.FakedObject);

        // act
        var result = await handler.ExecuteAsync(query, CancellationToken.None);

        // assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}