namespace Tests.Integration.TagsTests;

[Collection("Integration")]
public class ListTags(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestTagsAsync();

    [Fact]
    public async Task ListTags_Invoked_ReturnsTagListAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<ListTagsEndpoint, IEnumerable<GetTagResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
