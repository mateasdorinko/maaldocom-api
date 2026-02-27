namespace Tests.Unit.Application.Dtos.KnowledgeDtoTests;

public class ToString
{
    [Fact]
    public void ToString_WithTitleAndQuote_ReturnsTitleColonQuote()
    {
        // arrange
        var dto = new KnowledgeDto { Title = "Socrates", Quote = "Know thyself" };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe("Socrates:Know thyself");
    }

    [Fact]
    public void ToString_WithNullTitle_ReturnsNullColonQuote()
    {
        // arrange
        var dto = new KnowledgeDto { Title = null, Quote = "Know thyself" };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe(":Know thyself");
    }

    [Fact]
    public void ToString_WithNullQuote_ReturnsTitleColonNull()
    {
        // arrange
        var dto = new KnowledgeDto { Title = "Socrates", Quote = null };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe("Socrates:");
    }
}
