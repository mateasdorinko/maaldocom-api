namespace Tests.Unit.Application.Dtos.TagDtoTests;

public class ToString
{
    [Fact]
    public void ToString_WithName_ReturnsName()
    {
        // arrange
        var dto = new TagDto { Name = "nature" };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe("nature");
    }

    [Fact]
    public void ToString_WithNullName_ReturnsEmptyString()
    {
        // arrange
        var dto = new TagDto { Name = null };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe(string.Empty);
    }
}
