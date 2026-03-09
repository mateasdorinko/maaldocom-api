namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class ListMediaAlbums(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task ListMediaAlbums_Invoked_ReturnsMediaAlbumListAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<ListMediaAlbumsEndpoint, IEnumerable<GetMediaAlbumResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
