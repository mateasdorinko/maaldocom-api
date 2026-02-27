namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.PostMediaAlbumResponseTests;

public class AltHref
{
    [Fact]
    public void AltHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new PostMediaAlbumResponse { UrlFriendlyName = "test-album" };

        // act

        // assert
        model.AltHref.ShouldBe(UrlMaker.GetMediaAlbumUrl(model.UrlFriendlyName));
    }
}
