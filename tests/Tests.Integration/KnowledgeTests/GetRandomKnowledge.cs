namespace Tests.Integration.KnowledgeTests;

[Collection("Integration")]
public class GetRandomKnowledge(App app) : TestBase<App>
{
    [Fact]
    public async Task GetRandomKnowledge_Invoked_ReturnsRandomKnowledgeAndOk()
    {
        // arrange

        // act
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<GetRandomKnowledgeEndpoint, GetKnowledgeResponse>();

        // assert
        result.ShouldNotBeNull();
        result!.Title.ShouldNotBeNull();
        result.Quote.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
