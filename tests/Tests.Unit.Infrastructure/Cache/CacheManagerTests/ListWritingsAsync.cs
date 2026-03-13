namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class ListWritingsAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly MaaldoComDbContext _dbContext;
    private readonly CacheManager _sut;

    public ListWritingsAsync()
    {
        _dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(_dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task ListWritingsAsync_WithCachedData_ReturnsCachedData()
    {
        IEnumerable<WritingDto> cachedDtos = [new() { Id = Guid.NewGuid(), Title = "Cached" }];
        _cache.Setup(CacheKeys.WritingList, cachedDtos);

        var result = await _sut.ListWritingsAsync(TestContext.Current.CancellationToken);

        result.ShouldBe(cachedDtos);
    }

    [Fact]
    public async Task ListWritingsAsync_CacheMiss_FetchesFromDatabase()
    {
        var entity = new Writing { Id = Guid.NewGuid(), Title = "Test", Blurb = "test blurb", Slug = "test-slug", CreatedBy = "test-harness"};
        _dbContext.Writings.Add(entity);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _sut.ListWritingsAsync(TestContext.Current.CancellationToken);

        result.ShouldHaveSingleItem();
        result.First().Id.ShouldBe(entity.Id);
    }
}
