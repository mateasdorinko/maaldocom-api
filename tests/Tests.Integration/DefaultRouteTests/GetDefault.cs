using MaaldoCom.Api.Endpoints.Default;

namespace Tests.Integration.DefaultRouteTests;

public class GetDefault//(App app) : TestBase<App>
{
    [Fact(Skip = "Scaffolded, but need to setup integration test foundations.")]
    public async Task GetDefault_CONDITION_EXPECTATION()
    {
        await Task.Delay(0, TestContext.Current.CancellationToken);
        Assert.True(true);
    }
}
