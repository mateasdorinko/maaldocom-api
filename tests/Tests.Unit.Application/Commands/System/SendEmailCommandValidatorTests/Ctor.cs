using MaaldoCom.Api.Application.Commands.System;

namespace Tests.Unit.Application.Commands.System.SendEmailCommandValidatorTests;

public class Ctor
{
    [Fact]
    public async Task Ctor_WithValidCommand_PassesValidation()
    {
        // arrange
        var validator = new SendEmailCommandValidator();
        var command = new SendEmailCommand("user@example.com", "Hello", "Body text");

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public async Task Ctor_WithInvalidFromAddress_FailsValidation()
    {
        // arrange
        var validator = new SendEmailCommandValidator();
        var command = new SendEmailCommand("not-an-email", "Hello", "Body text");

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Valid email address is required");
    }

    [Fact]
    public async Task Ctor_WithEmptySubject_FailsValidation()
    {
        // arrange
        var validator = new SendEmailCommandValidator();
        var command = new SendEmailCommand("user@example.com", string.Empty, "Body text");

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Subject is required");
    }

    [Fact]
    public async Task Ctor_WithEmptyBody_FailsValidation()
    {
        // arrange
        var validator = new SendEmailCommandValidator();
        var command = new SendEmailCommand("user@example.com", "Hello", string.Empty);

        // act
        var result = await validator.ValidateAsync(command, TestContext.Current.CancellationToken);

        // assert
        result.IsValid.ShouldBe(false);
        result.Errors.ShouldContain(e => e.ErrorMessage == "Body is required");
    }
}
