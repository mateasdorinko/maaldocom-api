namespace Tests.Unit.Api.Endpoints.Knowledge.Models.GetKnowledgeResponseTests;

public class Href
{
    [Fact]
    public void Href_Invoked_ReturnsDetailUrl()
    {
        // arrange
        var model = new GetKnowledgeResponse { Id = Guid.NewGuid() };

        // act

        // assert
        model.Href.ShouldBe(UrlMaker.GetKnowledgeUrl(model.Id));
    }
}
