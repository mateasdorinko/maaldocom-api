using MaaldoCom.Services.Application.Messaging;
using MaaldoCom.Services.Application.Messaging.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MaaldoCom.Services.Application.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddMediator()
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(AssemblyReference))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
            services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));

            services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
            services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

            // services.Scan(scan => scan.FromAssembliesOf(typeof(AssemblyReference))
            //     .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
            //     .AsImplementedInterfaces()
            //     .WithScopedLifetime());

            services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
