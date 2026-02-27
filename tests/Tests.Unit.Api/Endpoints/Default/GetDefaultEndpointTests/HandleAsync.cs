namespace Tests.Unit.Api.Endpoints.Default.GetDefaultEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsHttpRedirectToDocs()
    {
        // arrange
        var endpoint = Factory.Create<GetDefaultEndpoint>();

        // act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.Redirect);
        endpoint.HttpContext.Response.Headers.Location.ShouldContain("/docs");
    }
}
