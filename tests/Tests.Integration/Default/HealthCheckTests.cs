using MaaldoCom.Services.Api.Endpoints.Default;

namespace Tests.Integration.Default;

public class HealthCheckTests//(App app) : TestBase<App>
{
    [Fact(Skip = "Scaffolded, but not implemented yet")]
    public async Task Get_HealthCheck_CONDITION_EXPECTATION()
    {
        await Task.Delay(0, CancellationToken.None);
        Assert.True(true);
        // arrange

        // act
        //var response = await app.Client.GETAsync<GetHealthCheckEndpoint, object>("/healthcheck");

        // assert
        //response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
