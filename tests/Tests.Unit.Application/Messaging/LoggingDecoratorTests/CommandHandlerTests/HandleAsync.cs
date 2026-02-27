using MaaldoCom.Api.Application.Messaging.Behaviors;

namespace Tests.Unit.Application.Messaging.LoggingDecoratorTests.CommandHandlerTests;

internal record TestCommand(string Data) : ICommand<string>;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WhenInnerHandlerSucceeds_ReturnsSuccessResult()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var logger = NullLogger<LoggingDecorator.CommandHandler<TestCommand, string>>.Instance;
        var ct = CancellationToken.None;
        var command = new TestCommand("data");
        var handler = new LoggingDecorator.CommandHandler<TestCommand, string>(innerHandler, logger);

        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok("success"));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe("success");
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WhenInnerHandlerFails_ReturnsFailedResult()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestCommand, string>>();
        var logger = NullLogger<LoggingDecorator.CommandHandler<TestCommand, string>>.Instance;
        var ct = CancellationToken.None;
        var command = new TestCommand("data");
        var handler = new LoggingDecorator.CommandHandler<TestCommand, string>(innerHandler, logger);

        A.CallTo(() => innerHandler.HandleAsync(command, ct))
            .Returns(Result.Fail<string>("command failed"));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].Message.ShouldBe("command failed");
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }
}
