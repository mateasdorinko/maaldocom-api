namespace Tests.Integration.WritingsTests;

[Collection("Integration")]
public class GetWritingById(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestWritingsAsync();

    [Fact]
    public async Task GetWritingById_ValidId_ReturnsWritingAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var writing = db.Writings.ElementAt(3);
        var request = new GetWritingByIdRequest { Id = writing.Id };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetWritingByIdEndpoint, GetWritingByIdRequest, GetWritingDetailResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(writing.Id);
        result.Title.ShouldBe(writing.Title);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetWritingById_InValidId_ReturnsNotFound()
    {
        // arrange
        var request = new GetWritingByIdRequest { Id = Guid.NewGuid() };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetWritingByIdEndpoint, GetWritingByIdRequest, GetWritingDetailResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
