namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class GetMediaAlbumDetailAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly CacheManager _sut;

    public GetMediaAlbumDetailAsync()
    {
        var dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task GetMediaAlbumDetailAsync_AlbumNotInListCache_ReturnsNull()
    {
        _cache.Setup<IEnumerable<MediaAlbumDto>>(CacheKeys.MediaAlbumList, []);

        var result = await _sut.GetMediaAlbumDetailAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }
}
