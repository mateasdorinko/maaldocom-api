namespace Tests.Unit.Domain.Helpers.SlugHelperTests;

public class GetSlugFromProperName
{
    [Fact]
    public void GetSlugFromProperName_Invoked_ReturnsSlug()
    {
        // arrange
        var properName = "Test Media Album";

        // act
        var result = SlugHelper.GetSlugFromProperName(properName);

        // assert
        result.ShouldBe("test-media-album");
    }
}
