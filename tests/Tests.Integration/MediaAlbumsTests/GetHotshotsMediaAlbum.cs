namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class GetHotshotsMediaAlbum(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task GetHotshotsMediaAlbum_Invoked_ReturnsHotShotsMediaAlbumAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetHotShotsMediaAlbumEndpoint, GetMediaAlbumDetailResponse>();

        // assert
        result.ShouldNotBeNull();
        result!.Name.ShouldBe("Hot Shots");
        result.UrlFriendlyName.ShouldBe("hotshots");
        result.Id.ShouldNotBe(Guid.Empty);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
