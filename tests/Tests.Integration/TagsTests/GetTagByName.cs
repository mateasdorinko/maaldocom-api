namespace Tests.Integration.TagsTests;

[Collection("Integration")]
public class GetTagByName(App app) : TestBase<App>
{
    [Fact]
    public async Task GetTagByName_ValidName_ReturnsTagAndOk()
    {
        // arrange
        await using var scope = app.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var tag = db.Tags.ElementAt(3);
        var request = new GetTagByNameRequest { Name = tag.Name! };

        // act
        var (response, result) = await app.GetUnauthorizedClient()
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
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<GetTagByNameEndpoint, GetTagByNameRequest, GetTagDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
