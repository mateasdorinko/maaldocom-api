namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.PostMediaAlbumResponseTests;

public class AltHref
{
    [Fact]
    public void AltHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new PostMediaAlbumResponse { Slug = "test-album" };

        // act

        // assert
        model.AltHref.ShouldBe(UrlMaker.GetMediaAlbumUrl(model.Slug));
    }
}
