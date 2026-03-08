using MaaldoCom.Api.Endpoints.Default;

namespace Tests.Integration.DefaultRouteTests;

[Collection("Integration")]
public class GetDefault(App app) : TestBase<App>
{
    [Fact]
    public async Task GetDefault_Invoked_RedirectsToDocsAndOk()
    {
        // arrange
        // app.CreateJwtToken(["role:admin"])

        // act
        var (response, _) = await app.Client.GETAsync<GetDefaultEndpoint, object>();

        // assert
        response.RequestMessage!.RequestUri!.AbsolutePath.ShouldContain("docs");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
