namespace Tests.Unit.Domain.MediaAlbums.MediaAlbumHelperTests;

public sealed class SanitizeFileName : IDisposable
{
    private readonly DirectoryInfo _dir = Directory.CreateTempSubdirectory();

    public void Dispose() => _dir.Delete(recursive: true);

    private FileInfo CreateFile(string name)
    {
        var path = Path.Combine(_dir.FullName, name);
        File.WriteAllBytes(path, []);
        return new FileInfo(path);
    }

    [Fact]
    public void SanitizeFileName_WithUnderScores_ReplacedWithDashes()
    {
        // arrange
        var fileInfo = CreateFile("test_file_name.jpg");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact]
    public void SanitizeFileName_WithSpaces_ReplacedWithDashes()
    {
        // arrange
        var fileInfo = CreateFile("test file name.jpg");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact]
    public void SanitizeFileName_WithUpperCase_ReplacedWithLowerCase()
    {
        // arrange
        var fileInfo = CreateFile("TesT_fILe_nAME.JPG");

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        fileInfo.Name.ShouldBe("test-file-name.jpg");
    }

    [Fact]
    public void SanitizeFileName_Invoked_MovesFileToNewLocation()
    {
        // arrange
        var fileInfo = CreateFile("test_file_name.jpg");
        var originalPath = fileInfo.FullName;

        // act
        MediaAlbumHelper.SanitizeFileName(fileInfo);

        // assert
        File.Exists(originalPath).ShouldBeFalse();
        File.Exists(Path.Combine(_dir.FullName, "test-file-name.jpg")).ShouldBeTrue();
    }
}
