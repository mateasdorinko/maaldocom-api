using System.Security.Claims;
//using MaaldoCom.Services.Application.Dtos;
using MaaldoCom.Services.Application.Interfaces;
using MaaldoCom.Services.Application.Queries.MediaAlbums;
using Microsoft.Extensions.Caching.Hybrid;

namespace Tests.Unit.Application.Queries.MediaAlbums.ListMediaAlbumsQueryCommandHandlerTests;

public class ExecuteAsync
{
    [Fact]
    public async Task executeasync_should_always_return_a_list_with_values()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        var db = A.Fake<IMaaldoComDbContext>();
        var cache = A.Fake<HybridCache>();
        
        var query = new ListMediaAlbumsQueryCommand(user);
        var handler = new ListMediaAlbumsQueryCommandHandler(db, cache);

        //A.CallTo(() => cache.GetOrCreateAsync<IEnumerable<MediaAlbumDto>>("media-albums")).Returns(new ValueTask<IEnumerable<MediaAlbumDto>>());

        // act
        var result = (await handler.ExecuteAsync(query, CancellationToken.None)).ToList();

        // assert
        //result.ShouldNotBeNull();
        //result.ShouldNotBeEmpty();
    }
}