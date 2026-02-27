namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetMediaAlbumByIdEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetMediaAlbumByIdEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
    }
}
