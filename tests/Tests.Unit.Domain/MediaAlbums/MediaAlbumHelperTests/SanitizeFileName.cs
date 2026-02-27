namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public class SanitizeFileName
{
    [Fact(Skip = "Need to figure out how to mock FileInfo")]
    public void SanitizeFileName_WithUnderScores_ReplacedWithDashes()
    {
        // arrange
        var fileInfo = A.Fake<FileInfo>();
        A.CallTo(() => fileInfo.Exists).Returns(true);
        A.CallTo(() => fileInfo.Name).Returns("test_file_name.jpg");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact(Skip = "Need to figure out how to mock FileInfo")]
    public void SanitizeFileName_WithSpaces_ReplacedWithDashes()
    {
        // arrange
        var fileInfo = A.Fake<FileInfo>();
        A.CallTo(() => fileInfo.Exists).Returns(true);
        A.CallTo(() => fileInfo.Name).Returns("test file name.jpg");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact(Skip = "Need to figure out how to mock FileInfo")]
    public void SanitizeFileName_WithUpperCase_ReplacedWithLowerCase()
    {
        // arrange
        var fileInfo = A.Fake<FileInfo>();
        A.CallTo(() => fileInfo.Exists).Returns(true);
        A.CallTo(() => fileInfo.Name).Returns("TesT_fILe_nAME.JPG");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact(Skip = "Need to figure out how to mock FileInfo")]
    public void SanitizeFileName_Invoked_MovesFileToNewLocation()
    {
        // arrange
        var fileInfo = A.Fake<FileInfo>();
        A.CallTo(() => fileInfo.Exists).Returns(true);
        A.CallTo(() => fileInfo.Name).Returns("test_file_name.jpg");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        A.CallTo(() => fileInfo.MoveTo(A<string>.That.Contains("test-file-name.jpg"), A<bool>._)).MustHaveHappened();
    }
}
