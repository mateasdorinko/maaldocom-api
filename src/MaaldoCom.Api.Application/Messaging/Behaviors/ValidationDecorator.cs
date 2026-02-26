using FluentValidation.Results;

namespace MaaldoCom.Api.Application.Messaging.Behaviors;

internal static class ValidationDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> innerHandler, IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken ct)
        {
            ValidationFailure[] validationFailures = await ValidateAsync(command, validators);

            if (validationFailures.Length == 0) { return await innerHandler.HandleAsync(command, ct); }

            return Result.Fail<TResponse>(validationFailures.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(ICommandHandler<TCommand> innerHandler, IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken ct)
        {
            ValidationFailure[] validationFailures = await ValidateAsync(command, validators);

            if (validationFailures.Length == 0) { return await innerHandler.HandleAsync(command, ct); }

            return Result.Fail(validationFailures.Select(IError (e) => new Error(e.ErrorMessage)).ToList());
        }
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command, IEnumerable<IValidator<TCommand>> validators)
    {
        if (!validators.Any()) { return []; }

        var context = new ValidationContext<TCommand>(command);

        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }
}
