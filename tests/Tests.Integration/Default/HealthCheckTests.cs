/*
using MaaldoCom.Services.Api.Endpoints.Default;

namespace Tests.Integration.Default;

public class HealthCheckTests(App app) : TestBase<App>
{
    [Fact]
    public async Task GetHealthCheck_ReturnsHealthyStatus()
    {
        // arrange

        // act
        var response = await app.Client.GETAsync<GetHealthCheckEndpoint, object>("/healthcheck");

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
*/
