namespace Tests.Integration.DefaultTests;

[Collection("Integration")]
public class GetHealthCheck(App app) : BaseIntegrationTest(app)
{
    [Fact]
    public async Task GetHealthCheck_Invoked_ReturnsHealthyAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetHealthCheckEndpoint, GetHealthCheckResponse>();

        // assert
        result.Status.ShouldBe("healthy");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
