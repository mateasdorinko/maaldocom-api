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

    protected Task AddTestKnowledgeAsync() => SeedDataAndInvalidateCacheAsync<Knowledge>(
    [
        new Knowledge { Title = "title-1", Quote = "quote-1" },
        new Knowledge { Title = "title-2", Quote = "quote-2" },
        new Knowledge { Title = "title-3", Quote = "quote-3" },
        new Knowledge { Title = "title-4", Quote = "quote-4" },
        new Knowledge { Title = "title-5", Quote = "quote-5" },
        new Knowledge { Title = "title-6", Quote = "quote-6" },
        new Knowledge { Title = "title-7", Quote = "quote-7" },
        new Knowledge { Title = "title-8", Quote = "quote-8" },
        new Knowledge { Title = "title-9", Quote = "quote-9" },
        new Knowledge { Title = "title-10", Quote = "quote-10" },
    ], CacheKeys.KnowledgeList);

    protected Task AddTestTagsAsync() => SeedDataAndInvalidateCacheAsync<Tag>(
    [
        new Tag { Name = "tag-1" },
        new Tag { Name = "tag-2" },
        new Tag { Name = "tag-3" },
        new Tag { Name = "tag-4" },
        new Tag { Name = "tag-5" },
        new Tag { Name = "tag-6" },
        new Tag { Name = "tag-7" },
        new Tag { Name = "tag-8" },
        new Tag { Name = "tag-9" },
        new Tag { Name = "tag-10" },
    ], CacheKeys.TagList);
}
