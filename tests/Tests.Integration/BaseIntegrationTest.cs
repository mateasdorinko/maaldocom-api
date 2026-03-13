using MaaldoCom.Api.Application.Cache;
using MaaldoCom.Api.Domain.Helpers;

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

        static MediaAlbum CreateMediaAlbum(string name, string slug, bool active)
            => new() { Name = name, Slug = slug, CreatedBy = "test-harness", Media = [], Active = active };
    }

    protected Task AddTestWritingsAsync()
    {
        return SeedDataAndInvalidateCacheAsync<Writing>(
        [
            CreateWriting("Writing 1", "writing-1", "Blurb for writing 1", "Body for writing 1", true),
            CreateWriting("Writing 2", "writing-2", "Blurb for writing 2", "Body for writing 2", true),
            CreateWriting("Writing 3", "writing-3", "Blurb for writing 3", "Body for writing 3", true),
            CreateWriting("Writing 4", "writing-4", "Blurb for writing 4", "Body for writing 4", true),
            CreateWriting("Writing 5", "writing-5", "Blurb for writing 5", "Body for writing 5", true),
            CreateWriting("Inactive Writing 1", "inactive-writing-1", "Blurb for inactive writing 1", "Body for inactive writing 1", false),
            CreateWriting("Inactive Writing 2", "inactive-writing-2", "Blurb for inactive writing 2", "Body for inactive writing 2", false),
            CreateWriting("Inactive Writing 3", "inactive-writing-3", "Blurb for inactive writing 3", "Body for inactive writing 3", false)
        ], CacheKeys.WritingList);

        static Writing CreateWriting(string title, string slug, string blurb, string body, bool active)
            => new() { Title = title, Slug = slug, Blurb = blurb, Body = body, CreatedBy = "test-harness", Active = active };
    }
}
