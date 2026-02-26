namespace MaaldoCom.Api.Application.Messaging.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken ct)
        {
            string commandName = typeof(TCommand).Name;

            logger.LogInformation("Processing command {Command}", commandName);

            Result<TResponse> result = await innerHandler.HandleAsync(command, ct);

            if (result.IsSuccess) { logger.LogInformation("Completed command {Command}", commandName); }
            else { logger.LogWarning("Completed command: {Command} with errors: {Error}", commandName, result.Errors.Select(e => e.Message).ToArray()); }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> HandleAsync(TCommand command, CancellationToken ct)
        {
            string commandName = typeof(TCommand).Name;

            logger.LogInformation("Processing command {Command}", commandName);

            Result result = await innerHandler.HandleAsync(command, ct);

            if (result.IsSuccess) { logger.LogInformation("Completed command {Command}", commandName); }
            else { logger.LogWarning("Completed command: {Command} with errors: {Error}", commandName, result.Errors.Select(e => e.Message).ToArray()); }

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken ct)
        {
            string queryName = typeof(TQuery).Name;

            logger.LogInformation("Processing query {Query}", queryName);

            Result<TResponse> result = await innerHandler.HandleAsync(query, ct);

            if (result.IsSuccess) { logger.LogInformation("Completed query {Query}", queryName); }
            else { logger.LogWarning("Completed query: {Query} with errors: {Error}", queryName, result.Errors.Select(e => e.Message).ToArray()); }

            return result;
        }
    }
}
