using System.Reflection;
using MaaldoCom.Services.Application.Messaging;
using MaaldoCom.Services.Application.Messaging.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MaaldoCom.Services.Application.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddMediator(params Assembly[] assemblies)
        {
            services.AddSingleton<IMediator, Mediator>();

            // Register handlers
            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                services.AddTransient(interfaceType, handlerType);
            }

            // Register notification handlers
            var notificationHandlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)))
                .ToList();

            foreach (var handlerType in notificationHandlerTypes)
            {
                var interfaces = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));

                foreach (var interfaceType in interfaces)
                {
                    services.AddTransient(interfaceType, handlerType);
                }
            }

            // Register validators from FluentValidation
            services.AddValidatorsFromAssemblies(assemblies);

            return services;
        }

        public IServiceCollection AddPipelineBehavior<TBehavior>() where TBehavior : class
        {
            // Register as open generic for all request types
            var behaviorType = typeof(TBehavior);

            if (!behaviorType.IsGenericTypeDefinition)
            {
#pragma warning disable S3928
                throw new ArgumentException("TBehavior must be an open generic type", nameof(TBehavior));
#pragma warning restore S3928
            }

            services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
            return services;
        }
    }
}
