using MaaldoCom.Api.Application.Email;
using MaaldoCom.Api.Application.Messaging;

namespace MaaldoCom.Api.Application.Commands.System;

public sealed record SendEmailCommand(string From, string Subject, string Body) : ICommand<EmailResponse>;

internal sealed class SendEmailCommandHandler(IEmailProvider emailProvider) : ICommandHandler<SendEmailCommand, EmailResponse>
{
    public async Task<Result<EmailResponse>> HandleAsync(SendEmailCommand command, CancellationToken ct)
    {
        var response = await emailProvider.SendEmailAsync(command.From, command.Subject, command.Body, ct);

        return Result.Ok(response);
    }
}

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(c => c.From)
            .EmailAddress().WithMessage("Valid email address is required");
        RuleFor(c => c.Subject)
            .NotEmpty().WithMessage("Subject is required");
        RuleFor(c => c.Body)
            .NotEmpty().WithMessage("Body is required");
    }
}
