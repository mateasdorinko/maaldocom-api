namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Mediator interface for sending requests and publishing notifications.
/// </summary>
public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
}
