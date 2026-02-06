namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class GetOriginalMetaFilePath
{
    [Fact]
    public void GetOriginalMetaFilePath_InvokedWithJpg_ReturnsOriginal()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.jpg";

        // act
        var result = MediaAlbumHelper.GetOriginalMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/original/{originalFileName}");
    }

    [Fact]
    public void GetOriginalMetaFilePath_InvokedWithPng_ReturnsOriginal()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.png";

        // act
        var result = MediaAlbumHelper.GetOriginalMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/original/{originalFileName}");
    }

    [Fact]
    public void GetOriginalMetaFilePath_InvokedWithMp4_ReturnsOriginal()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.mp4";

        // act
        var result = MediaAlbumHelper.GetOriginalMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/original/{originalFileName}");
    }
}
