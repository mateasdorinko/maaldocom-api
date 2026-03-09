using MaaldoCom.Api.Application.Cache;

namespace Tests.Integration;

public abstract class BaseIntegrationTest(App app) : TestBase<App>, IAsyncLifetime
{
    protected App App { get; } = app;

    async ValueTask IAsyncLifetime.InitializeAsync()
    {
        await App.ResetDatabaseAsync();
        await SetupAsync();
    }

    ValueTask IAsyncDisposable.DisposeAsync() => ValueTask.CompletedTask;

    private async Task SeedDataAndInvalidateCacheAsync<TEntity>(IEnumerable<TEntity> entities, string cacheToInvalidate) where TEntity : class
    {
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();
        db.Set<TEntity>().AddRange(entities);
        await db.SaveChangesAsync();

        var cacheManager = scope.ServiceProvider.GetRequiredService<ICacheManager>();
        await cacheManager.InvalidateCache(cacheToInvalidate, CancellationToken.None);
    }

    protected Task AddTestKnowledgeAsync()
    {
        return SeedDataAndInvalidateCacheAsync<Knowledge>(
            [
                CreateKnowledge("title-1", "quote-1"),
                CreateKnowledge("title-2", "quote-2"),
                CreateKnowledge("title-3", "quote-3"),
                CreateKnowledge("title-4", "quote-4"),
                CreateKnowledge("title-5", "quote-5")
            ], CacheKeys.KnowledgeList);

        static Knowledge CreateKnowledge(string title, string quote) => new() { Title = title, Quote = quote };
    }

    protected Task AddTestTagsAsync()
    {
        return SeedDataAndInvalidateCacheAsync<Tag>(
            [
                CreateTag("tag-1"),
                CreateTag("tag-2"),
                CreateTag("tag-3"),
                CreateTag("tag-4"),
                CreateTag("tag-5")
            ], CacheKeys.TagList);

        static Tag CreateTag(string name) => new() { Name = name };
    }

    protected Task AddTestMediaAlbumsAsync()
    {
        return SeedDataAndInvalidateCacheAsync<MediaAlbum>(
        [
            CreateMediaAlbum("Hot Shots", "hotshots", true),
            CreateMediaAlbum("Media Album 1", "mediaalbum-1", true),
            CreateMediaAlbum("Media Album 2", "mediaalbum-2", true),
            CreateMediaAlbum("Media Album 3", "mediaalbum-3", true),
            CreateMediaAlbum("Media Album 4", "mediaalbum-4", true),
            CreateMediaAlbum("Media Album 5", "mediaalbum-5", true),
            CreateMediaAlbum("Inactive Media Album 1", "inactive-mediaalbum-1", false),
            CreateMediaAlbum("Inactive Media Album 2", "inactive-mediaalbum-2", false),
            CreateMediaAlbum("Inactive Media Album 3", "inactive-mediaalbum-3", false)
        ], CacheKeys.MediaAlbumList);

        static MediaAlbum CreateMediaAlbum(string name, string urlFriendlyName, bool active)
            => new() { Name = name, UrlFriendlyName = urlFriendlyName, CreatedBy = "test-harness", Media = [], Active = active };
    }
}
