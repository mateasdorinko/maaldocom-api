namespace Tests.Unit.Domain.Entities.MediaTests;

public class ToString
{
    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var entity = new Media
        {
            FileName = "test-file.jpg",
            MediaAlbum = new MediaAlbum
            {
                Name = "Test Media Album"
            }
        };

        // Act
        var result = entity.ToString();

        // Assert
        result.ShouldBe($"{entity.MediaAlbum.Name}:{entity.FileName}");
    }
}
