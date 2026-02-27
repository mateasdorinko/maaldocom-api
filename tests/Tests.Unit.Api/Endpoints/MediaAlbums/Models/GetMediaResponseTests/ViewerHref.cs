namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.GetMediaResponseTests;

public class ViewerHref
{
    [Fact]
    public void ViewerHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetMediaResponse { MediaAlbumId = Guid.NewGuid(), Id = Guid.NewGuid() };

        // act

        // assert
        model.ViewerHref.ShouldBe(UrlMaker.GetViewerMediaUrl(model.MediaAlbumId, model.Id));
    }
}
