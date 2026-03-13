namespace Tests.Integration.WritingsTests;

[Collection("Integration")]
public class ListWritings(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestWritingsAsync();

    [Fact]
    public async Task ListWritings_Invoked_ReturnsWritingListAndOk()
    {
        // arrange

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<ListWritingsEndpoint, IEnumerable<GetWritingResponse>>();

        // assert
        result.ShouldNotBeEmpty();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
