namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class InvalidateCache : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly CacheManager _sut;

    public InvalidateCache()
    {
        var dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task InvalidateCache_WithKey_CallsRemoveAsync()
    {
        await _sut.InvalidateCache(CacheKeys.KnowledgeList, TestContext.Current.CancellationToken);

        _cache.RemovedKeys.ShouldContain(CacheKeys.KnowledgeList);
    }
}
