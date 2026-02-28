using Microsoft.Extensions.DependencyInjection;

namespace Tests.Unit.Application.Extensions.ServiceCollectionExtensionsTests;

public sealed class AddApplicationServices
{
    [Fact]
    public void AddApplicationServices_RegistersExpectedServices()
    {
        var services = new ServiceCollection();

        services.AddApplicationServices();

        services.Any(sd => sd.ServiceType.IsGenericType && sd.ServiceType.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)).ShouldBeTrue();
        services.Any(sd => sd.ServiceType.IsGenericType && sd.ServiceType.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)).ShouldBeTrue();
        services.Any(sd => sd.ServiceType.IsGenericType && sd.ServiceType.GetGenericTypeDefinition() == typeof(IValidator<>)).ShouldBeTrue();
    }
}
