using FluentValidation.Results;
using MaaldoCom.Api.Application.Messaging.Behaviors;

namespace Tests.Unit.Application.Messaging.ValidationDecoratorTests.CommandHandlerTests;

internal record TestCommand(string Data) : ICommand<string>;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithNoValidators_DelegatesToInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var ct = TestContext.Current.CancellationToken;
        var command = new TestCommand("data");
        var handler = new ValidationDecorator.CommandHandler<TestCommand, string>(innerHandler, []);

        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok("success"));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe("success");
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithPassingValidator_DelegatesToInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var validator = A.Fake<IValidator<TestCommand>>();
        var ct = TestContext.Current.CancellationToken;
        var command = new TestCommand("data");
        var handler = new ValidationDecorator.CommandHandler<TestCommand, string>(innerHandler, [validator]);

        A.CallTo(() => validator.ValidateAsync(A<ValidationContext<TestCommand>>._, A<CancellationToken>._))
            .Returns(new ValidationResult());
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok("success"));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe("success");
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithFailingValidator_ReturnsFailedResultWithoutCallingInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var validator = A.Fake<IValidator<TestCommand>>();
        var ct = TestContext.Current.CancellationToken;
        var command = new TestCommand("data");
        var handler = new ValidationDecorator.CommandHandler<TestCommand, string>(innerHandler, [validator]);

        var failures = new ValidationResult([new ValidationFailure("Data", "Data is invalid")]);
        A.CallTo(() => validator.ValidateAsync(A<ValidationContext<TestCommand>>._, A<CancellationToken>._))
            .Returns(failures);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].Message.ShouldBe("Data is invalid");
        A.CallTo(() => innerHandler.HandleAsync(A<TestCommand>._, ct)).MustNotHaveHappened();
    }

    [Fact]
    public async Task HandleAsync_WithMultipleFailingValidators_ReturnsAllErrors()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var validator1 = A.Fake<IValidator<TestCommand>>();
        var validator2 = A.Fake<IValidator<TestCommand>>();
        var ct = TestContext.Current.CancellationToken;
        var command = new TestCommand("data");
        var handler = new ValidationDecorator.CommandHandler<TestCommand, string>(innerHandler, [validator1, validator2]);

        A.CallTo(() => validator1.ValidateAsync(A<ValidationContext<TestCommand>>._, A<CancellationToken>._))
            .Returns(new ValidationResult([new ValidationFailure("Data", "Error from validator1")]));
        A.CallTo(() => validator2.ValidateAsync(A<ValidationContext<TestCommand>>._, A<CancellationToken>._))
            .Returns(new ValidationResult([new ValidationFailure("Data", "Error from validator2")]));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors.Count.ShouldBe(2);
    }
}
