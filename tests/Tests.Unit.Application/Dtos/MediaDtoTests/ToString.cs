namespace Tests.Unit.Application.Dtos.MediaDtoTests;

public class ToString
{
    [Fact]
    public void ToString_WithFileName_ReturnsFileName()
    {
        // arrange
        var dto = new MediaDto { FileName = "photo.jpg" };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe("photo.jpg");
    }

    [Fact]
    public void ToString_WithNullFileName_ReturnsNull()
    {
        // arrange
        var dto = new MediaDto { FileName = null };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBeNull();
    }
}
