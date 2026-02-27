namespace Tests.Unit.Api.Endpoints.System.PostEmailEndpointTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidParams_ReturnsCreated()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<SendEmailCommand, EmailResponse>>();
        var linkGenerator = A.Fake<LinkGenerator>();
        var result = new Result<EmailResponse>();

        A.CallTo(() => handler.HandleAsync(A<SendEmailCommand>.Ignored, A<CancellationToken>.Ignored)).Returns(result);
        A.CallTo(() => linkGenerator.GetPathByAddress(
                A<HttpContext>.Ignored,
                A<string>.Ignored,
                A<RouteValueDictionary>.Ignored,
                A<RouteValueDictionary>.Ignored,
                A<PathString?>.Ignored,
                A<FragmentString>.Ignored,
                A<LinkOptions?>.Ignored))
            .Returns(A.Dummy<string>());

        var endpoint = Factory.Create<PostMailEndpoint>(ctx => ctx.AddTestServices(s => s.AddSingleton(linkGenerator)),
            handler);

        // act
        await endpoint.HandleAsync(A.Dummy<PostMailRequest>(), CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.Created);
        endpoint.HttpContext.Response.Headers.ContainsKey("Location").ShouldBeTrue();
    }

    [Fact]
    public async Task HandleAsync_WithInValidParams_ReturnsBadRequestWithBrokenRules()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<SendEmailCommand, EmailResponse>>();
        var result = Result.Fail(new[] { new Error("Invalid data") });

        A.CallTo(() => handler.HandleAsync(A<SendEmailCommand>.Ignored, A<CancellationToken>.Ignored)).Returns(result);

        var endpoint = Factory.Create<PostMailEndpoint>(handler);

        // act
        await endpoint.HandleAsync(A.Dummy<PostMailRequest>(), CancellationToken.None);

        // assert
        endpoint.HttpContext.Response.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
        endpoint.HttpContext.Response.Headers.ContainsKey("Location").ShouldBeFalse();
        endpoint.ValidationFailures.ShouldHaveSingleItem();
        endpoint.ValidationFailures.ShouldContain(f => f.ErrorMessage == result.Errors[0].Message);
    }
}
