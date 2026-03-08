using MaaldoCom.Api.Endpoints.Default;
using MaaldoCom.Api.Endpoints.Default.Models;

namespace Tests.Integration.DefaultRouteTests;

[Collection("Integration")]
public class GetHealthCheck(App app) : TestBase<App>
{
    [Fact]
    public async Task GetHealthCheck_Invoked_ReturnsHealthyAndOk()
    {
        // arrange

        // act
        var (response, result) = await app.Client.GETAsync<GetHealthCheckEndpoint, GetHealthCheckResponse>();

        // assert
        result.Status.ShouldBe("healthy");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
