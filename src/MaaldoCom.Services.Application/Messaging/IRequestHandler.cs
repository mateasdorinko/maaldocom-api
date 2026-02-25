namespace MaaldoCom.Services.Application.Messaging;

/// <summary>
/// Handler interface for processing requests.
/// </summary>
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
