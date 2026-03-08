namespace Tests.Integration.KnowledgeTests;

[Collection("Integration")]
public class ListKnowledge(App app) : TestBase<App>
{
    [Fact]
    public async Task ListKnowledge_Invoked_ReturnsKnowledgeListAndOk()
    {
        // arrange

        // act
        var (response, result) = await app.Client.GETAsync<ListKnowledgeEndpoint, IEnumerable<GetKnowledgeResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
