namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class ListTagsAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly MaaldoComDbContext _dbContext;
    private readonly CacheManager _sut;

    public ListTagsAsync()
    {
        _dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(_dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task ListTagsAsync_WithCachedData_ReturnsCachedData()
    {
        IEnumerable<TagDto> cachedDtos = [new() { Id = Guid.NewGuid(), Name = "Cached" }];
        _cache.Setup(CacheKeys.TagList, cachedDtos);

        var result = await _sut.ListTagsAsync(TestContext.Current.CancellationToken);

        result.ShouldBe(cachedDtos);
    }

    [Fact]
    public async Task ListTagsAsync_CacheMiss_FetchesFromDatabase()
    {
        var entity = new Tag { Id = Guid.NewGuid(), Name = "Test" };
        _dbContext.Tags.Add(entity);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _sut.ListTagsAsync(TestContext.Current.CancellationToken);

        result.ShouldHaveSingleItem();
        result.First().Id.ShouldBe(entity.Id);
    }
}
