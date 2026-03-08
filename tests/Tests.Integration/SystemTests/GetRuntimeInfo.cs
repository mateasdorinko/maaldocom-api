namespace Tests.Integration.SystemTests;

[Collection("Integration")]
public class GetRuntimeInfo(App app) : TestBase<App>
{
    [Fact]
    public async Task GetRuntimeInfo_Unauthorized_ReturnsUnauthorized()
    {
        // arrange

        // act
        var (response, result) = await app.Client.GETAsync<GetRuntimeInfoEndpoint, GetRuntimeInfoResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetRuntimeInfo_Authorized_ReturnsRuntimeInfoAndOk()
    {
        // arrange
        var client = app.CreateClientWithPermissions(["read:runtime-info"]);

        // act
        var (response, result) = await client.GETAsync<GetRuntimeInfoEndpoint, GetRuntimeInfoResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
    }
}
