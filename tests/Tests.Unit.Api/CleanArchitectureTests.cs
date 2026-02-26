using NetArchTest.Rules;

namespace Tests.Unit.Api;

public class CleanArchitectureTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Api.Domain.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn("MaaldoCom.Api.Endpoints")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Api.Application.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn("MaaldoCom.Api.Endpoints")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Api.Infrastructure.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn("MaaldoCom.Api.Endpoints")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
