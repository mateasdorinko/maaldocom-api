using MaaldoCom.Api.Application.Messaging.Behaviors;

namespace Tests.Unit.Application.Messaging.LoggingDecoratorTests.CommandBaseHandlerTests;

internal record TestBaseCommand : ICommand;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WhenInnerHandlerSucceeds_ReturnsSuccessResult()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestBaseCommand>>();
        var logger = NullLogger<LoggingDecorator.CommandBaseHandler<TestBaseCommand>>.Instance;
        var ct = TestContext.Current.CancellationToken;
        var command = new TestBaseCommand();
        var handler = new LoggingDecorator.CommandBaseHandler<TestBaseCommand>(innerHandler, logger);

        A.CallTo(() => innerHandler.HandleAsync(command, ct)).Returns(Result.Ok());

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task HandleAsync_WhenInnerHandlerFails_ReturnsFailedResult()
    {
        // arrange
        var innerHandler = A.Fake<ICommandHandler<TestBaseCommand>>();
        var logger = NullLogger<LoggingDecorator.CommandBaseHandler<TestBaseCommand>>.Instance;
        var ct = TestContext.Current.CancellationToken;
        var command = new TestBaseCommand();
        var handler = new LoggingDecorator.CommandBaseHandler<TestBaseCommand>(innerHandler, logger);

        A.CallTo(() => innerHandler.HandleAsync(command, ct))
            .Returns(Result.Fail("command failed"));

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsFailed.ShouldBe(true);
        result.Errors[0].Message.ShouldBe("command failed");
        A.CallTo(() => innerHandler.HandleAsync(command, ct)).MustHaveHappenedOnceExactly();
    }
}
