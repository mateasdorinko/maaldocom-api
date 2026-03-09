namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class PostMediaAlbum(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact(Skip = "Not implemented yet")]
    public async Task PostMediaAlbum_Y_Z()
    {
        // arrange

        // act

        // assert
    }
}
