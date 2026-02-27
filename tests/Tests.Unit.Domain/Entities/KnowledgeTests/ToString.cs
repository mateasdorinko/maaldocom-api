namespace Tests.Unit.Domain.Entities.KnowledgeTests;

public class ToString
{
    [Fact]
    public void ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var entity = new Knowledge
        {
            Title = "Test Knowledge",
            Quote = "This is a test knowledge entity."
        };

        // Act
        var result = entity.ToString();

        // Assert
        result.ShouldBe($"{entity.Title}:{entity.Quote}");
    }
}
