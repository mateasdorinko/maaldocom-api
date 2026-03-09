namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class GetMediaAlbumById(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task GetMediaAlbumById_ValidId_ReturnsMediaAlbumAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var mediaAlbum = db.MediaAlbums.ElementAt(3);
        var request = new GetMediaAlbumByIdRequest { Id = mediaAlbum.Id };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumByIdEndpoint, GetMediaAlbumByIdRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(mediaAlbum.Id);
        result.Name.ShouldBe(mediaAlbum.Name);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMediaAlbumById_InValidId_ReturnsNotFound()
    {
        // arrange
        var request = new GetMediaAlbumByIdRequest { Id = Guid.NewGuid() };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetMediaAlbumByIdEndpoint, GetMediaAlbumByIdRequest, GetMediaAlbumDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
