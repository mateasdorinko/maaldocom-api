namespace Tests.Integration.SystemTests;

[Collection("Integration")]
public class GetRuntimeInfo(App app) : BaseIntegrationTest(app)
{
    [Fact]
    public async Task GetRuntimeInfo_Unauthorized_ReturnsUnauthorized()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetRuntimeInfoEndpoint, GetRuntimeInfoResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetRuntimeInfo_Authorized_ReturnsRuntimeInfoAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetAuthorizedClient(["read:runtime-info"])
            .GETAsync<GetRuntimeInfoEndpoint, GetRuntimeInfoResponse>();

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldNotBeNull();
    }
}
