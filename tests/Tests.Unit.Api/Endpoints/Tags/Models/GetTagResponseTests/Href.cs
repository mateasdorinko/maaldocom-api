namespace Tests.Unit.Api.Endpoints.Tags.Models.GetTagResponseTests;

public class Href
{
    [Fact]
    public void AltHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetTagResponse { Id = Guid.NewGuid() };

        // act

        // assert
        model.Href.ShouldBe(UrlMaker.GetTagUrl(model.Id));
    }
}
