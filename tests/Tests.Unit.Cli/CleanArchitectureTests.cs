using NetArchTest.Rules;

namespace Tests.Unit.Cli;

public class CleanArchitectureTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Domain.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Cli.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Application.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Cli.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Infrastructure.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Cli.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
