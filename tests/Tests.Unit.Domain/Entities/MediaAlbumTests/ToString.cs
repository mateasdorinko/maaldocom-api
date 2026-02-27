namespace Tests.Unit.Domain.Entities.MediaAlbumTests;

public class ToString
{
    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var entity = new MediaAlbum
        {
            Name = "Test Media Album"
        };

        // Act
        var result = entity.ToString();

        // Assert
        result.ShouldBe($"{entity.Name}");
    }
}
