using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaaldoCom.Api.Infrastructure.Extensions;

namespace Tests.Unit.Infrastructure.Extensions.ServiceCollectionExtensionsTests;

public sealed class AddInfrastructureServices
{
    [Fact]
    public void AddInfrastructureServices_RegistersExpectedServices()
    {
        var services = new ServiceCollection();
        var config = A.Fake<IConfiguration>();

        services.AddInfrastructureServices(config);

        services.Any(sd => sd.ServiceType == typeof(ICacheManager)).ShouldBeTrue();
        services.Any(sd => sd.ServiceType == typeof(IBlobsProvider)).ShouldBeTrue();
        services.Any(sd => sd.ServiceType == typeof(IEmailProvider)).ShouldBeTrue();
    }
}
