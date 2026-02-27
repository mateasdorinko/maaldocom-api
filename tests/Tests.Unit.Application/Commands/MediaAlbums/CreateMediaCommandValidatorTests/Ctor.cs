using MaaldoCom.Api.Application.Commands.MediaAlbums;

namespace Tests.Unit.Application.Commands.MediaAlbums.CreateMediaCommandValidatorTests;

public class Ctor
{
    [Fact]
    public async Task Ctor_WithValidCommand_PassesValidation()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        var validator = new CreateMediaCommandValidator(dbContext);
        var command = new CreateMediaCommand(
            new ClaimsPrincipal(),
            new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg" });

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithInvalidMedia_FailsValidation()
    {
        // arrange
        var dbContext = A.Fake<IMaaldoComDbContext>();
        var validator = new CreateMediaCommandValidator(dbContext);
        var command = new CreateMediaCommand(
            new ClaimsPrincipal(),
            new MediaDto { FileName = string.Empty, FileExtension = ".jpg" });

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media fileName is required");
    }
}
