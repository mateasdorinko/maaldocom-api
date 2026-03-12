namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.GetMediaAlbumResponseTests;

public class AltHref
{
    [Fact]
    public void AltHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetMediaAlbumResponse { Slug = "test-album" };

        // act

        // assert
        model.AltHref.ShouldBe(UrlMaker.GetMediaAlbumUrl(model.Slug));
    }
}
