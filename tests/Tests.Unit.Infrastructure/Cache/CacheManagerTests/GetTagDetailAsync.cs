namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class GetTagDetailAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly CacheManager _sut;

    public GetTagDetailAsync()
    {
        var dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task GetTagDetailAsync_TagNotInListCache_ReturnsNull()
    {
        _cache.Setup<IEnumerable<TagDto>>(CacheKeys.TagList, []);

        var result = await _sut.GetTagDetailAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        result.ShouldBeNull();
    }
}
