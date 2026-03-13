namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class PostMediaAlbum(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact]
    public async Task PostMediaAlbum_Unauthorized_ReturnsUnauthorized()
    {
        // arrange
        var request = new PostMediaAlbumRequest();

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .POSTAsync<PostMediaAlbumEndpoint, PostMediaAlbumRequest, PostMediaAlbumResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        result.ShouldBeNull();
    }

    /*[Fact]
    public async Task PostMediaAlbum_AuthorizedWithInvalidRequest_ReturnsBadRequest()
    {
        // arrange
        var request = new PostMediaAlbumRequest();

        // act
        var (response, result) = await App.GetAuthorizedClient(["write:media-albums"])
            .POSTAsync<PostMediaAlbumEndpoint, PostMediaAlbumRequest, PostMediaAlbumResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.ShouldBeNull();
    }*/
}
