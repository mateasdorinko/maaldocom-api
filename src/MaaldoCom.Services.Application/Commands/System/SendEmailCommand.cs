using MaaldoCom.Services.Application.Email;

namespace MaaldoCom.Services.Application.Commands.System;

public class SendEmailCommand(string from, string subject, string body) : ICommand<Result<EmailResponse>>
{
    public string From { get; } = from;
    public string Subject { get; } = subject;
    public string Body { get; } = body;
}

public class SendEmailCommandHandler(IEmailProvider emailProvider) : ICommandHandler<SendEmailCommand, Result<EmailResponse>>
{
    public async Task<Result<EmailResponse>> ExecuteAsync(SendEmailCommand command, CancellationToken ct)
    {
        var validationResult = await new SendEmailCommandValidator().ValidateAsync(command, ct);

        if (!validationResult.IsValid)
        {
            return Result.Fail<EmailResponse>(validationResult.Errors.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }

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
