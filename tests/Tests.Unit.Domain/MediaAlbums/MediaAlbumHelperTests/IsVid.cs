namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class IsVid
{
    [Fact]
    public void IsVid_ValidVidFile_ReturnsTrue()
    {
        // arrange
        var validVids = new []
            { "sample.mp4", "sample.mov", "sample.avi", "sample.mkv", "sample.wmv", "sample.flv", "sample.webm" };

        // act
        var results = validVids.Select(MediaAlbumHelper.IsVid) ;

        // assert
        results.All(x => x).ShouldBeTrue();
    }

    [Fact]
    public void IsVid_InvalidVidFile_ReturnsFalse()
    {
        // arrange
        var inValidVids = new []
            { "sample.jpg", "sample.jpeg", "sample.png", "sample.gif", "sample.bmp", "sample.tiff", "sample.webp" };

        // act
        var results = inValidVids.Select(MediaAlbumHelper.IsVid) ;

        // assert
        results.All(x => x).ShouldBeFalse();
    }
}
