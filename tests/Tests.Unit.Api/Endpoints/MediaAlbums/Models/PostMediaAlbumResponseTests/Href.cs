namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.PostMediaAlbumResponseTests;

public class Href
{
    [Fact]
    public void Href_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new PostMediaAlbumResponse { Id = Guid.NewGuid() };

        // act

        // assert
        model.Href.ShouldBe(UrlMaker.GetMediaAlbumUrl(model.Id));
    }
}
