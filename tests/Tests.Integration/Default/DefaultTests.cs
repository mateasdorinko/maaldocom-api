using MaaldoCom.Api.Endpoints.Default;

namespace Tests.Integration.Default;

public class DefaultTests//(App app) : TestBase<App>
{
    [Fact(Skip = "Scaffolded, but need to setup integration test foundations.")]
    public async Task Get_Default_CONDITION_EXPECTATION()
    {
        await Task.Delay(0, CancellationToken.None);
        Assert.True(true);
    }
}
