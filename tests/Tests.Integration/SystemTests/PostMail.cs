namespace Tests.Integration.SystemTests;

[Collection("Integration")]
public class PostMail(App app) : BaseIntegrationTest(app)
{
    [Fact]
    public async Task PostMail_Unauthorized_ReturnsUnauthorized()
    {
        // arrange
        var request = new PostMailRequest { From = "a@b.com", Subject = "test subject", Body = "test body" };

        // act
        var (response, _) = await App.GetUnauthorizedClient()
            .POSTAsync<PostMailEndpoint, PostMailRequest, object>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task PostMail_AuthorizedWithValidRequest_ReturnsCreated()
    {
        // arrange
        var request = new PostMailRequest { From = "a@b.com", Subject = "test subject", Body = "test body" };

        // act
        var (response, _) = await App.GetAuthorizedClient(["write:emails"]).POSTAsync<PostMailEndpoint, PostMailRequest, object>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostMail_AuthorizedWithInValidRequest_ReturnsBadRequest()
    {
        // arrange
        var request = new PostMailRequest { From = string.Empty, Subject = string.Empty, Body = string.Empty };

        // act
        var (response, result) = await App.GetAuthorizedClient(["write:emails"])
            .POSTAsync<PostMailEndpoint, PostMailRequest, ProblemDetailsResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Message.ShouldContain("One or more errors occurred!");
        result.Errors.GeneralErrors.Count.ShouldBe(3);
        result.Errors.GeneralErrors.ShouldContain("Valid email address is required");
        result.Errors.GeneralErrors.ShouldContain("Subject is required");
        result.Errors.GeneralErrors.ShouldContain("Body is required");
    }

    [Fact]
    public async Task PostMail_AuthorizedWithInValidEmail_ReturnsBadRequest()
    {
        // arrange
        var request = new PostMailRequest { From = string.Empty, Subject = "test subject", Body = "test body" };

        // act
        var (response, result) = await App.GetAuthorizedClient(["write:emails"])
            .POSTAsync<PostMailEndpoint, PostMailRequest, ProblemDetailsResponse>(request);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Message.ShouldContain("One or more errors occurred!");
        result.Errors.GeneralErrors.Count.ShouldBe(1);
        result.Errors.GeneralErrors.ShouldContain("Valid email address is required");
    }
}
