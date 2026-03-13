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

    [Fact]
    public async Task PostMediaAlbum_AuthorizedWithValidRequest_ReturnsCreated()
    {
        // arrange
        var request = new PostMediaAlbumRequest
        {
            Name = "Valid Test Album For Create",
            Slug = "valid-test-album-for-create",
            Description = "Valid Test Album for Create",
            Media = [new PostMediaRequest { FileName = "asdf.jpg", FileExtension = ".jpg" }], Tags = []
        };

        // act
        var (response, result) = await App.GetAuthorizedClient(["write:media-albums"])
            .POSTAsync<PostMediaAlbumEndpoint, PostMediaAlbumRequest, PostMediaAlbumResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostMediaAlbum_AuthorizedWithInvalidRequest_ReturnsBadRequest()
    {
        // arrange
        var request = new PostMediaAlbumRequest { Media = [], Tags = [] };

        // act
        var (response, result) = await App.GetAuthorizedClient(["write:media-albums"])
            .POSTAsync<PostMediaAlbumEndpoint, PostMediaAlbumRequest, ProblemDetailsResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Message.ShouldContain("One or more errors occurred!");
        result.Errors.GeneralErrors.Count.ShouldBe(4);
        result.Errors.GeneralErrors.ShouldContain("Media album name is required");
        result.Errors.GeneralErrors.ShouldContain("Media album slug is required");
        result.Errors.GeneralErrors.ShouldContain("Media album description is required");
        result.Errors.GeneralErrors.ShouldContain("Media album media list cannot be empty");
    }
}
