namespace Tests.Unit.Api.Endpoints.Knowledge.ListKnowledgeEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>>>();
        var endpoint = Factory.Create<ListKnowledgeEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.Routes.ShouldHaveSingleItem();
        endpoint.Definition.Routes.ShouldContain(UrlMaker.KnowledgeRoute);
    }
}
