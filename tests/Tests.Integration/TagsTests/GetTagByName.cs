namespace Tests.Integration.TagsTests;

[Collection("Integration")]
public class GetTagByName(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestTagsAsync();

    [Fact]
    public async Task GetTagByName_ValidName_ReturnsTagAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var tag = db.Tags.ElementAt(3);
        var request = new GetTagByNameRequest { Name = tag.Name! };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetTagByNameEndpoint, GetTagByNameRequest, GetTagDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(tag.Id);
        result.Name.ShouldBe(tag.Name);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetTagByName_InValidId_ReturnsNotFound()
    {
        // arrange
        var request = new GetTagByNameRequest { Name = "non-existent-tag" };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetTagByNameEndpoint, GetTagByNameRequest, GetTagDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
