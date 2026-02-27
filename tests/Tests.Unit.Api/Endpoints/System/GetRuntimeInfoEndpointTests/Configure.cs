namespace Tests.Unit.Api.Endpoints.System.GetRuntimeInfoEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var endpoint = Factory.Create<GetRuntimeInfoEndpoint>();

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs.ShouldBeNull();
        endpoint.Definition.AllowedPermissions!.ShouldContain("read:runtime-info");
    }
}
