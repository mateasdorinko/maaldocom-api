namespace Tests.Unit.Api.Endpoints.System.GetRuntimeInfoEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsRuntimeInfo()
    {
        // arrange
        var endpoint = Factory.Create<GetRuntimeInfoEndpoint>();

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldBeOfType<GetRuntimeInfoResponse>();
        response.ClrVersion.ShouldBe(Environment.Version.ToString());
        response.Is64BitProcess.ShouldBe(Environment.Is64BitProcess);
        response.Is64BitSystem.ShouldBe(Environment.Is64BitOperatingSystem);
        response.MachineName.ShouldBe(Environment.MachineName);
        response.OsVersion.ShouldBe(Environment.OSVersion.ToString());
        response.ProcessId.ShouldBe(Environment.ProcessId);
        response.ProcessorCount.ShouldBe(Environment.ProcessorCount);
        response.ProcessPath.ShouldBe(Environment.ProcessPath);
        response.AspNetCoreEnvironment.ShouldBe(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        response.User.ShouldBe("guest");
    }
}
