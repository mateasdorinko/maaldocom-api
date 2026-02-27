namespace Tests.Unit.Api.Endpoints.Knowledge.GetKnowledgeByIdEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetKnowledgeQuery, KnowledgeDto>>();
        var endpoint = Factory.Create<GetKnowledgeByIdEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.Routes.ShouldHaveSingleItem();
    }
}
