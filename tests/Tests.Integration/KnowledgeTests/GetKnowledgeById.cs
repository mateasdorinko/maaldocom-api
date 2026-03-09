namespace Tests.Integration.KnowledgeTests;

[Collection("Integration")]
public class GetKnowledgeById(App app) : TestBase<App>
{
    [Fact]
    public async Task GetKnowledgeById_Invoked_ReturnsKnowledgeByIdAndOk()
    {
        // arrange
        await using var scope = app.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<MaaldoComDbContext>();

        var knowledge = db.Knowledge.ElementAt(10);
        var request = new GetKnowledgeByIdRequest { Id = knowledge.Id };

        // act
        var (response, result) = await app.GetUnauthorizedClient()
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
        var (response, result) = await app.GetUnauthorizedClient()
            .GETAsync<GetKnowledgeByIdEndpoint, GetKnowledgeByIdRequest, GetKnowledgeResponse>(request);

        // assert
        result.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
