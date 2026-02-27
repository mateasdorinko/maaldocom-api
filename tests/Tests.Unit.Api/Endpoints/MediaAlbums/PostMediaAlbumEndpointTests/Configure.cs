namespace Tests.Unit.Api.Endpoints.MediaAlbums.PostMediaAlbumEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<MaaldoCom.Api.Application.Messaging.ICommandHandler<CreateMediaAlbumCommand, MediaAlbumDto>>();
        var endpoint = Factory.Create<PostMediaAlbumEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Post.Method);
        endpoint.Definition.AnonymousVerbs.ShouldBeNull();
        endpoint.Definition.AllowedPermissions!.ShouldContain("write:media-albums");
    }
}
