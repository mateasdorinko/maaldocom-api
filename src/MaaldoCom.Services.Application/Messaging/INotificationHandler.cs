namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Handler interface for processing notifications.
/// </summary>
public interface INotificationHandler<in TNotification> where TNotification : INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}
