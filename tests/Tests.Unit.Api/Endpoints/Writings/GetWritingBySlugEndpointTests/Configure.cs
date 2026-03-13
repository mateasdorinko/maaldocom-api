namespace Tests.Unit.Api.Endpoints.Writings.GetWritingBySlugEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetWritingDetailQuery, WritingDto>>();
        var endpoint = Factory.Create<GetWritingBySlugEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
    }
}
