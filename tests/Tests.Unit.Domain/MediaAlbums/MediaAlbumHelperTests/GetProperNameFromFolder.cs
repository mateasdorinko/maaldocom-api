namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class GetProperNameFromFolder
{
    [Fact]
    public void GetProperNameFromFolder_Invoked_ReturnsProperName()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";

        // act
        var result = MediaAlbumHelper.GetProperNameFromFolder(mediaALbumFolder);

        // assert
        result.ShouldBe("Test Media Album");
    }
}
