namespace Tests.Integration.TagsTests;

[Collection("Integration")]
public class ListTags(App app) : TestBase<App>
{
    [Fact]
    public async Task ListTags_Invoked_ReturnsTagListAndOk()
    {
        // arrange

        // act
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<ListTagsEndpoint, IEnumerable<GetTagResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
