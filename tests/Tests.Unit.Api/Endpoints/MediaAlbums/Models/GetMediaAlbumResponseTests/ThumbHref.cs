namespace Tests.Unit.Api.Endpoints.MediaAlbums.Models.GetMediaAlbumResponseTests;

public class ThumbHref
{
    [Fact]
    public void ThumbHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetMediaAlbumResponse { Id = Guid.NewGuid(), DefaultMediaId = Guid.NewGuid() };

        // act

        // assert
        model.ThumbHref.ShouldBe(UrlMaker.GetThumbnailMediaUrl(model.Id, model.DefaultMediaId));
    }
}
