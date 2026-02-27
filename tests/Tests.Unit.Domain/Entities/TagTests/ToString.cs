namespace Tests.Unit.Domain.Entities.TagTests;

public class ToString
{
    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var entity = new Tag()
        {
            Name = "test-tag",
        };

        // Act
        var result = entity.ToString();

        // Assert
        result.ShouldBe($"{entity.Name}");
    }
}
