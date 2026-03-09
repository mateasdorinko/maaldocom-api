namespace Tests.Integration.MediaAlbumsTests;

[Collection("Integration")]
public class GetMediaById(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestMediaAlbumsAsync();

    [Fact(Skip = "Not implemented yet")]
    public async Task GetMediaById_Y_Z()
    {
        // arrange

        // act

        // assert
    }
}
