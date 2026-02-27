namespace Tests.Unit.Api.Endpoints.Tags.Models.GetTagResponseTests;

public class AltHref
{
    [Fact]
    public void AltHref_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetTagResponse { Name = "tag1" };

        // act

        // assert
        model.AltHref.ShouldBe(UrlMaker.GetTagUrl(model.Name));
    }
}
