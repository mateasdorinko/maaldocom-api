namespace Tests.Integration.DefaultTests;

[Collection("Integration")]
public class GetDefault(App app) : TestBase<App>
{
    [Fact]
    public async Task GetDefault_Invoked_RedirectsToDocsAndOk()
    {
        // arrange

        // act
        var (response, _) = await app.GetUnauthorizedClient()
            .GETAsync<GetDefaultEndpoint, object>();

        // assert
        response.RequestMessage!.RequestUri!.AbsolutePath.ShouldContain("docs");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
