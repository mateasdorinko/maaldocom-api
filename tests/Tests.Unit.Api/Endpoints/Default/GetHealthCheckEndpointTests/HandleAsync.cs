using MaaldoCom.Api.Endpoints.Default;

namespace Tests.Unit.Api.Endpoints.Default.GetHealthCheckEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsHealthy()
    {
        // arrange
        var endpoint = Factory.Create<GetHealthCheckEndpoint>();

        // act
        await endpoint.HandleAsync(CancellationToken.None);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe(200);
        response.ShouldNotBeNull();
    }
}
