using MaaldoCom.Api.Application.Commands.System;
using MaaldoCom.Api.Application.Email;

namespace Tests.Unit.Application.Commands.System.SendEmailCommandHandlerTests;

public class HandleAsync
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_SendsEmailAndReturnsResponse()
    {
        // arrange
        var emailProvider = A.Fake<IEmailProvider>();
        var ct = TestContext.Current.CancellationToken;
        var command = new SendEmailCommand("user@example.com", "Hello", "Body text");
        var handler = new SendEmailCommandHandler(emailProvider);

        var expectedResponse = new EmailResponse { IsSuccessStatusCode = true };
        A.CallTo(() => emailProvider.SendEmailAsync(command.From, command.Subject, command.Body, ct))
            .Returns(expectedResponse);

        // act
        var result = await handler.HandleAsync(command, ct);

        // assert
        result.IsSuccess.ShouldBe(true);
        result.Value.ShouldBe(expectedResponse);
        A.CallTo(() => emailProvider.SendEmailAsync(command.From, command.Subject, command.Body, ct))
            .MustHaveHappenedOnceExactly();
    }
}
