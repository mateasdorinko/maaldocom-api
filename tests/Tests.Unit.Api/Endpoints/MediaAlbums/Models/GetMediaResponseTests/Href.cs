namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.GetMediaResponseTests;

public class Href
{
    [Fact]
    public void Href_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetMediaResponse { MediaAlbumId = Guid.NewGuid(), Id = Guid.NewGuid() };

        // act

        // assert
        model.Href.ShouldBe(UrlMaker.GetOriginalMediaUrl(model.MediaAlbumId, model.Id));
    }
}
