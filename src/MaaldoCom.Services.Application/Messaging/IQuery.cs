namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Marker interface for queries (read-only operations).
/// Queries skip transaction behaviors.
/// </summary>
public interface IQuery<TResponse> : IRequest<TResponse> { }
