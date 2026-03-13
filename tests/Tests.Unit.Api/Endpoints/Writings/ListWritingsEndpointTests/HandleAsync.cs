namespace Tests.Unit.Api.Endpoints.Writings.ListWritingsEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_Invoked_ReturnsWritingList()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListWritingsQuery, IEnumerable<WritingDto>>>();
        var endpoint = Factory.Create<ListWritingsEndpoint>(handler);
        var result = new Result<IEnumerable<WritingDto>>().WithValue(new List<WritingDto>());

        A.CallTo(() => handler.HandleAsync(A<ListWritingsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response.ShouldNotBeNull();
        response.ShouldBeOfType<List<GetWritingResponse>>();
    }

    [Fact]
    public async Task HandleAsync_WithInactiveWritings_ExcludesInactiveWritings()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<ListWritingsQuery, IEnumerable<WritingDto>>>();
        var endpoint = Factory.Create<ListWritingsEndpoint>(handler);
        var activeWriting = new WritingDto { Id = Guid.NewGuid(), Title = "Active Writing", Slug = "active-writing", Active = true };
        var inactiveWriting = new WritingDto { Id = Guid.NewGuid(), Title = "Inactive Writing", Slug = "inactive-writing", Active = false };
        var result = new Result<IEnumerable<WritingDto>>().WithValue([activeWriting, inactiveWriting]);

        A.CallTo(() => handler.HandleAsync(A<ListWritingsQuery>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        // act
        await endpoint.HandleAsync(TestContext.Current.CancellationToken);
        var response = endpoint.Response as List<GetWritingResponse>;

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        response!.Count.ShouldBe(1);
        response[0].Title.ShouldBe(activeWriting.Title);
    }
}
