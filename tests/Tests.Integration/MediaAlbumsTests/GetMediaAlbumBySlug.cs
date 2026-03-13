namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class GetMediaAlbumBySlug(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task GetMediaAlbumBySlug_ValidName_ReturnsMediaAlbumAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var mediaAlbum = db.MediaAlbums.Where(ma => ma.Active).ElementAt(3);
        var request = new GetMediaAlbumBySlugRequest { Slug = mediaAlbum!.Slug! };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumBySlugEndpoint, GetMediaAlbumBySlugRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(mediaAlbum.Id);
        result.Name.ShouldBe(mediaAlbum.Name);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMediaAlbumBySlug_InValidName_ReturnsNotFound()
    {
        // arrange
        var request = new GetMediaAlbumBySlugRequest { Slug = "non-existent-mediaalbum" };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumBySlugEndpoint, GetMediaAlbumBySlugRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
