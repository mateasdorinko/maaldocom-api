using NetArchTest.Rules;

namespace Tests.Unit.Infrastructure;

public class CleanArchitectureTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Domain.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Infrastructure.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Application.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Infrastructure.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
