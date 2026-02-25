namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Marker interface for commands (write operations).
/// Commands implement transaction behaviors.
/// </summary>
public interface ICommand<TResponse> : IRequest<TResponse> { }
