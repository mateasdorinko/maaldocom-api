namespace Tests.Integration.KnowledgeTests;

[Collection("Integration")]
public class ListKnowledge(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestKnowledgeAsync();

    [Fact]
    public async Task ListKnowledge_Invoked_ReturnsKnowledgeListAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<ListKnowledgeEndpoint, IEnumerable<GetKnowledgeResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
