namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class ListKnowledgeAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly MaaldoComDbContext _dbContext;
    private readonly CacheManager _sut;

    public ListKnowledgeAsync()
    {
        _dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(_dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task ListKnowledgeAsync_WithCachedData_ReturnsCachedData()
    {
        IEnumerable<KnowledgeDto> cachedDtos = [new KnowledgeDto { Id = Guid.NewGuid(), Title = "Cached" }];
        _cache.Setup(CacheKeys.KnowledgeList, cachedDtos);

        var result = await _sut.ListKnowledgeAsync(TestContext.Current.CancellationToken);

        result.ShouldBe(cachedDtos);
    }

    [Fact]
    public async Task ListKnowledgeAsync_CacheMiss_FetchesFromDatabase()
    {
        var entity = new Knowledge { Id = Guid.NewGuid(), Title = "Test", Quote = "Test quote" };
        _dbContext.Knowledge.Add(entity);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _sut.ListKnowledgeAsync(TestContext.Current.CancellationToken);

        result.ShouldHaveSingleItem();
        result.First().Id.ShouldBe(entity.Id);
    }
}
