namespace Tests.Unit.Api.Endpoints.Writings.GetWritingByIdEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidId_ReturnsWriting()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetWritingDetailQuery, WritingDto>>();
        var endpoint = Factory.Create<GetWritingByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = new Result<WritingDto>().WithValue(new WritingDto { Id = id });

        A.CallTo(() => handler.HandleAsync(A<GetWritingDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetWritingByIdRequest { Id = id }, TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.Id.ShouldBe(id);
        response.ShouldBeOfType<GetWritingDetailResponse>();
    }

    [Fact]
    public async Task HandleAsync_WithInValidId_ReturnsNotFound()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetWritingDetailQuery, WritingDto>>();
        var endpoint = Factory.Create<GetWritingByIdEndpoint>(handler);
        var id = Guid.NewGuid();
        var result = Result.Fail(A.Dummy<string>());

        A.CallTo(() => handler.HandleAsync(A<GetWritingDetailQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(new GetWritingByIdRequest { Id = id }, TestContext.Current.CancellationToken);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
    }
}
