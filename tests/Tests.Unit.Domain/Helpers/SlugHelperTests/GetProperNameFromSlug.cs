namespace Tests.Unit.Domain.Helpers.SlugHelperTests;

public class GetProperNameFromSlug
{
    [Fact]
    public void GetProperNameFromSlug_Invoked_ReturnsProperName()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";

        // act
        var result = SlugHelper.GetProperNameFromSlug(mediaALbumFolder);

        // assert
        result.ShouldBe("Test Media Album");
    }
}
