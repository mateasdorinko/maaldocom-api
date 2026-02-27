using MaaldoCom.Api.Application.Dtos.Validators;

namespace Tests.Unit.Application.Dtos.Validators.CreateMediaValidatorTests;

public class Ctor
{
    [Fact]
    public async Task Ctor_WithValidMedia_PassesValidation()
    {
        // arrange
        var validator = new CreateMediaValidator();
        var media = new MediaDto { FileName = "photo.jpg", FileExtension = ".jpg" };

        // act
        var result = await validator.ValidateAsync(media, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithEmptyFileName_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaValidator();
        var media = new MediaDto { FileName = string.Empty, FileExtension = ".jpg" };

        // act
        var result = await validator.ValidateAsync(media, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media fileName is required");
    }

    [Fact]
    public async Task Ctor_WithFileNameExceedingMaxLength_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaValidator();
        var media = new MediaDto { FileName = new string('a', 51), FileExtension = ".jpg" };

        // act
        var result = await validator.ValidateAsync(media, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media fileName must be 50 characters or less");
    }

    [Fact]
    public async Task Ctor_WithEmptyFileExtension_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaValidator();
        var media = new MediaDto { FileName = "photo.jpg", FileExtension = string.Empty };

        // act
        var result = await validator.ValidateAsync(media, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media fileExtension is required");
    }

    [Fact]
    public async Task Ctor_WithFileExtensionExceedingMaxLength_FailsValidation()
    {
        // arrange
        var validator = new CreateMediaValidator();
        var media = new MediaDto { FileName = "photo.jpg", FileExtension = new string('a', 21) };

        // act
        var result = await validator.ValidateAsync(media, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Media fileExtension must be 20 characters or less");
    }
}
