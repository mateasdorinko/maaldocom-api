namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class IsPic
{
    [Fact]
    public void IsPic_ValidPicFile_ReturnsTrue()
    {
        // arrange
        var validPics = new []
            { "sample.jpg", "sample.jpeg", "sample.png", "sample.gif", "sample.bmp", "sample.tiff", "sample.webp" };

        // act
        var results = validPics.Select(MediaAlbumHelper.IsPic) ;

        // assert
        results.All(x => x).ShouldBeTrue();
    }

    [Fact]
    public void IsPic_InvalidPicFile_ReturnsFalse()
    {
        // arrange
        var inValidPics = new []
            { "sample.mp4", "sample.mov", "sample.avi", "sample.mkv", "sample.wmv", "sample.flv", "sample.webm" };

        // act
        var results = inValidPics.Select(MediaAlbumHelper.IsPic) ;

        // assert
        results.All(x => x).ShouldBeFalse();
    }
}
