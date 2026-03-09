namespace Tests.Integration.SystemTests;

[Collection("Integration")]
public class RefreshCache(App app) : BaseIntegrationTest(app)
{
    [Fact]
    public async Task RefreshCache_Invoked_ReturnsCreated()
    {
        // arrange
        var hotShotsAlbum = new MediaAlbum
        {
            Name = "HotShots", UrlFriendlyName = "hotshots", CreatedBy = "test-harness", Media = []
        };
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        await db.MediaAlbums.AddAsync(hotShotsAlbum, TestContext.Current.CancellationToken);
        await db.SaveChangesAsync(TestContext.Current.CancellationToken);

        // act
        var (response, _) = await App.GetUnauthorizedClient()
            .POSTAsync<PostCacheRefreshEndpoint, object>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
