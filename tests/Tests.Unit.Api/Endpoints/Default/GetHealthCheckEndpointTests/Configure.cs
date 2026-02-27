namespace Tests.Unit.Api.Endpoints.Default.GetHealthCheckEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var endpoint = Factory.Create<GetHealthCheckEndpoint>();

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.Routes.ShouldHaveSingleItem();
        endpoint.Definition.Routes.ShouldContain(UrlMaker.GetHealthCheckUrl());
    }
}
