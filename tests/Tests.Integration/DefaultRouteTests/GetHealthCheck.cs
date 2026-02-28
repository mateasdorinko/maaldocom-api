using MaaldoCom.Api.Endpoints.Default;

namespace Tests.Integration.DefaultRouteTests;

public class GetHealthCheck//(App app) : TestBase<App>
{
    [Fact(Skip = "Scaffolded, but need to setup integration test foundations.")]
    public async Task Get_HealthCheck_CONDITION_EXPECTATION()
    {
        await Task.Delay(0, TestContext.Current.CancellationToken);
        Assert.True(true);
        // arrange

        // act
        //var response = await app.Client.GETAsync<GetHealthCheckEndpoint, object>("/healthcheck");

        // assert
        //response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
