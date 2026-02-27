using MaaldoCom.Api.Application.Dtos.Validators;

namespace Tests.Unit.Application.Dtos.Validators.TagValidatorTests;

public class Ctor
{
    [Fact]
    public async Task Ctor_WithValidTag_PassesValidation()
    {
        // arrange
        var validator = new TagValidator();
        var tag = new TagDto { Name = "nature" };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithEmptyName_FailsValidation()
    {
        // arrange
        var validator = new TagValidator();
        var tag = new TagDto { Name = string.Empty };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Tag name cannot be empty");
    }

    [Fact]
    public async Task Ctor_WithNameExceedingMaxLength_FailsValidation()
    {
        // arrange
        var validator = new TagValidator();
        var tag = new TagDto { Name = new string('a', 21) };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Tag name must be 20 characters or less");
    }

    [Fact]
    public async Task Ctor_WithNameAtMaxLength_PassesValidation()
    {
        // arrange
        var validator = new TagValidator();
        var tag = new TagDto { Name = new string('a', 20) };

        // act
        var result = await validator.ValidateAsync(tag, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }
}
