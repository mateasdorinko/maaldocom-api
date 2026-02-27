using MaaldoCom.Api.Application.Email;

namespace Tests.Unit.Api.Endpoints.System.PostEmailEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<SendEmailCommand, EmailResponse>>();
        var endpoint = Factory.Create<PostMailEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Post.Method);
        endpoint.Definition.AnonymousVerbs.ShouldBeNull();
        endpoint.Definition.AllowedPermissions!.ShouldContain("write:emails");
    }
}
