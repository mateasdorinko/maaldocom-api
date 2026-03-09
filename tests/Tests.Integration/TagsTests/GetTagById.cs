namespace Tests.Integration.TagsTests;

[Collection("Integration")]
public class GetTagById(App app) : TestBase<App>
{
    [Fact]
    public async Task GetTagById_ValidId_ReturnsTagAndOk()
    {
        // arrange
        await using var scope = app.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var tag = db.Tags.ElementAt(3);
        var request = new GetTagByIdRequest { Id = tag.Id };

        // act
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<GetTagByIdEndpoint, GetTagByIdRequest, GetTagDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(tag.Id);
        result.Name.ShouldBe(tag.Name);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetTagById_InValidId_ReturnsNotFound()
    {
        // arrange
        var request = new GetTagByIdRequest { Id = Guid.NewGuid() };

        // act
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<GetTagByIdEndpoint, GetTagByIdRequest, GetTagDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
