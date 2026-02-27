using FluentValidation.Results;
using MaaldoCom.Api.Application.Messaging.Behaviors;

namespace Tests.Unit.Application.Messaging.ValidationDecoratorTests.CommandBaseHandlerTests;

internal record TestBaseCommand : ICommand;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithNoValidators_DelegatesToInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestBaseCommand>>();
        var ct = CancellationToken.None;
        var command = new TestBaseCommand();
        var handler = new ValidationDecorator.CommandBaseHandler<TestBaseCommand>(innerHandler, []);

        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok());

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithPassingValidator_DelegatesToInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestBaseCommand>>();
        var validator = A.Fake<IValidator<TestBaseCommand>>();
        var ct = CancellationToken.None;
        var command = new TestBaseCommand();
        var handler = new ValidationDecorator.CommandBaseHandler<TestBaseCommand>(innerHandler, [validator]);

        A.CallTo(() => validator.ValidateAsync(A<ValidationContext<TestBaseCommand>>._, ct))
            .Returns(new ValidationResult());
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok());

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WithFailingValidator_ReturnsFailedResultWithoutCallingInnerHandler()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestBaseCommand>>();
        var validator = A.Fake<IValidator<TestBaseCommand>>();
        var ct = CancellationToken.None;
        var command = new TestBaseCommand();
        var handler = new ValidationDecorator.CommandBaseHandler<TestBaseCommand>(innerHandler, [validator]);

        var failures = new ValidationResult([new ValidationFailure("Field", "Field is required")]);
        A.CallTo(() => validator.ValidateAsync(A<ValidationContext<TestBaseCommand>>._, ct))
            .Returns(failures);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].Message.ShouldBe("Field is required");
        A.CallTo(() => innerHandler.HandleAsync(A<TestBaseCommand>._, ct)).MustNotHaveHappened();
    }
}
