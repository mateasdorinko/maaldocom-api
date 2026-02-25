using MaaldoCom.Services.Application.Messaging.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Mediator implementation that routes requests to handlers through a pipeline of behaviors.
/// </summary>
public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        // Get the handler
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for request type {requestType.Name}");

        // Get all pipeline behaviors for this request type
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
        var behaviors = serviceProvider
            .GetServices(behaviorType)
            .Cast<object>()
            .Reverse()
            .ToList();

        // Build the pipeline - start with the handler
        var handleMethod = handlerType.GetMethod("HandleAsync")!;
        RequestHandlerDelegate<TResponse> pipeline = () =>
        {
            var task = (Task<TResponse>)handleMethod.Invoke(handler, [request, cancellationToken])!;
            return task;
        };

        // Wrap with behaviors (in reverse order so first registered runs first)
        foreach (var behavior in behaviors)
        {
            var currentPipeline = pipeline;
            var behaviorHandleMethod = behaviorType.GetMethod("HandleAsync")!;

            pipeline = () =>
            {
                var task = (Task<TResponse>)behaviorHandleMethod.Invoke(behavior, [request, currentPipeline, cancellationToken])!;
                return task;
            };
        }

        return await pipeline();
    }

    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var notificationType = notification.GetType();
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);

        var handlers = serviceProvider.GetServices(handlerType);

        var tasks = handlers.Select(handler =>
        {
            var handleMethod = handlerType.GetMethod("HandleAsync")!;
            return (Task)handleMethod.Invoke(handler, [notification, cancellationToken])!;
        });

        await Task.WhenAll(tasks);
    }
}
