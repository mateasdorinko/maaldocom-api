namespace Tests.Integration.WritingsTests;

[Collection("Integration")]
public class GetWritingBySlug(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestWritingsAsync();

    [Fact]
    public async Task GetWritingBySlug_ValidName_ReturnsWritingAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var writing = db.Writings.Where(ma => ma.Active).ElementAt(3);
        var request = new GetWritingBySlugRequest { Slug = writing!.Slug! };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetWritingBySlugEndpoint, GetWritingBySlugRequest, GetWritingDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(writing.Id);
        result.Title.ShouldBe(writing.Title);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetWritingBySlug_InValidName_ReturnsNotFound()
    {
        // arrange
        var request = new GetWritingBySlugRequest { Slug = "non-existent-writing" };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetWritingBySlugEndpoint, GetWritingBySlugRequest, GetWritingDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
