namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class GetMediaAlbumByName(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task GetMediaAlbumByName_ValidName_ReturnsMediaAlbumAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var mediaAlbum = db.MediaAlbums.Where(ma => ma.Active).ElementAt(3);
        var request = new GetMediaAlbumByNameRequest { Name = mediaAlbum!.UrlFriendlyName! };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumByNameEndpoint, GetMediaAlbumByNameRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(mediaAlbum.Id);
        result.Name.ShouldBe(mediaAlbum.Name);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMediaAlbumByName_InValidName_ReturnsNotFound()
    {
        // arrange
        var request = new GetMediaAlbumByNameRequest { Name = "non-existent-mediaalbum" };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumByNameEndpoint, GetMediaAlbumByNameRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
