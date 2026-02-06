namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class GetViewerMetaFilePath
{
    [Fact]
    public void GetViewerMetaFilePath_InvokedWithJpg_ReturnsViewerJpg()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.jpg";

        // act
        var result = MediaAlbumHelper.GetViewerMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/viewer/viewer-{originalFileName}");
    }

    [Fact]
    public void GetViewerMetaFilePath_InvokedWithPng_ReturnsViewerPng()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.png";

        // act
        var result = MediaAlbumHelper.GetViewerMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/viewer/viewer-{originalFileName}");
    }

    [Fact]
    public void GetViewerMetaFilePath_InvokedWithMp4_ReturnsViewerMp4()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.mp4";

        // act
        var result = MediaAlbumHelper.GetViewerMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/original/test-file.mp4");
    }
}
