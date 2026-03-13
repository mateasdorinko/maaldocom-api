namespace Tests.Unit.Infrastructure.Cache.CacheManagerTests;

public sealed class ListMediaAlbumsAsync : IDisposable
{
    private readonly TestHybridCache _cache = new();
    private readonly MaaldoComDbContext _dbContext;
    private readonly CacheManager _sut;

    public ListMediaAlbumsAsync()
    {
        _dbContext = DbContextFactory.CreateInMemory();
        var factory = DbContextFactory.CreateFactory(_dbContext);
        _sut = new CacheManager(factory, _cache);
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task ListMediaAlbumsAsync_WithCachedData_ReturnsCachedData()
    {
        IEnumerable<MediaAlbumDto> cachedDtos = [new() { Id = Guid.NewGuid(), Name = "Cached" }];
        _cache.Setup(CacheKeys.MediaAlbumList, cachedDtos);

        var result = await _sut.ListMediaAlbumsAsync(TestContext.Current.CancellationToken);

        result.ShouldBe(cachedDtos);
    }

    [Fact]
    public async Task ListMediaAlbumsAsync_CacheMiss_FetchesFromDatabase()
    {
        var entity = new MediaAlbum { Id = Guid.NewGuid(), Name = "Test", Slug = "test-media-album", CreatedBy = "test-harness"};
        _dbContext.MediaAlbums.Add(entity);
        await _dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var result = await _sut.ListMediaAlbumsAsync(TestContext.Current.CancellationToken);

        result.ShouldHaveSingleItem();
        result.First().Id.ShouldBe(entity.Id);
    }
}
