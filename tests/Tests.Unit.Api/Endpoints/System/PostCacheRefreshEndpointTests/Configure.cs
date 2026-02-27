namespace Tests.Unit.Api.Endpoints.System.PostCacheRefreshEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<CacheRefreshCommand>>();
        var endpoint = Factory.Create<PostCacheRefreshEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Post.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Post.Method);
    }
}
