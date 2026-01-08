using Microsoft.Extensions.DependencyInjection;

namespace MaaldoCom.Services.Application.Messaging;

public class Sender(IServiceProvider provider) : ISender
{
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = provider.GetRequiredService(handlerType);

        return handler.Handle((dynamic)request, cancellationToken);
    }
}