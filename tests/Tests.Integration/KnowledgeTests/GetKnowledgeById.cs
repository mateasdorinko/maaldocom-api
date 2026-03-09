namespace Tests.Integration.KnowledgeTests;

[Collection("Integration")]
public class GetKnowledgeById(App app) : BaseIntegrationTest(app)
{
    protected override async ValueTask SetupAsync() => await AddTestKnowledgeAsync();

    [Fact]
    public async Task GetKnowledgeById_Invoked_ReturnsKnowledgeByIdAndOk()
    {
        // arrange
        await using var scope = App.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();
        var knowledge = db.Knowledge.ElementAt(2);

        var request = new GetKnowledgeByIdRequest { Id = knowledge.Id };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetKnowledgeByIdEndpoint, GetKnowledgeByIdRequest, GetKnowledgeResponse>(request);

        // assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(knowledge.Id);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetKnowledgeById_InvalidId_ReturnsNotFound()
    {
        // arrange
        var request = new GetKnowledgeByIdRequest { Id = Guid.NewGuid() };

        // act
        var (response, result) = await App.GetUnauthorizedClient()
            .GETAsync<GetKnowledgeByIdEndpoint, GetKnowledgeByIdRequest, GetKnowledgeResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
