namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class GetThumbnailMetaFilePath
{
    [Fact]
    public void GetThumbnailMetaFilePath_InvokedWithJpg_ReturnsThumbnailJpg()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.jpg";

        // act
        var result = MediaAlbumHelper.GetThumbnailMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/thumb/thumb-{originalFileName}");
    }

    [Fact]
    public void GetThumbnailMetaFilePath_InvokedWithPng_ReturnsThumbnailPng()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.png";

        // act
        var result = MediaAlbumHelper.GetThumbnailMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/thumb/thumb-{originalFileName}");
    }

    [Fact]
    public void GetThumbnailMetaFilePath_InvokedWithMp4_ReturnsThumbnailJpg()
    {
        // arrange
        var mediaALbumFolder = "test-media-album";
        var originalFileName = "test-file.mp4";

        // act
        var result = MediaAlbumHelper.GetThumbnailMetaFilePath(mediaALbumFolder, originalFileName);

        // assert
        result.ShouldBe($"{mediaALbumFolder}/thumb/thumb-test-file.jpg");
    }
}
