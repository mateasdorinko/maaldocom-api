namespace Tests.Unit.Api.Endpoints.Knowledge.GetRandomKnowledgeEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetRandomKnowledgeQuery, KnowledgeDto>>();
        var endpoint = Factory.Create<GetRandomKnowledgeEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.Routes.ShouldHaveSingleItem();
    }
}
