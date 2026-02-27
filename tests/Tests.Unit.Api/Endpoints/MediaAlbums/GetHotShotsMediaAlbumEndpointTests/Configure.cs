namespace Tests.Unit.Api.Endpoints.MediaAlbums.GetHotShotsMediaAlbumEndpointTests;

public class Configure
{
    [Fact]
    public void Configure_Invoked_SetsUpEndpointCorrectly()
    {
        // arrange
        var handler = A.Fake<IQueryHandler<GetHotshotsMediaAlbumDetailQuery, MediaAlbumDto>>();
        var endpoint = Factory.Create<GetHotShotsMediaAlbumEndpoint>(handler);

        // act
        endpoint.Configure();

        // assert
        endpoint.Definition.Verbs.ShouldHaveSingleItem();
        endpoint.Definition.Verbs.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.AnonymousVerbs!.ShouldContain(HttpMethod.Get.Method);
        endpoint.Definition.Routes.ShouldHaveSingleItem();
    }
}
