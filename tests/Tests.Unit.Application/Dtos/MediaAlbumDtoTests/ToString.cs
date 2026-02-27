namespace Tests.Unit.Application.Dtos.MediaAlbumDtoTests;

public class ToString
{
    [Fact]
    public void ToString_WithName_ReturnsName()
    {
        // arrange
        var dto = new MediaAlbumDto { Name = "Summer Trip 2024" };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBe("Summer Trip 2024");
    }

    [Fact]
    public void ToString_WithNullName_ReturnsNull()
    {
        // arrange
        var dto = new MediaAlbumDto { Name = null };

        // act
        var result = dto.ToString();

        // assert
        result.ShouldBeNull();
    }
}
