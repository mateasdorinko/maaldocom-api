using System.Reflection;

namespace Tests.Unit;

public class LayerTests
{
    private readonly Assembly _infrastructureAssembly = MaaldoCom.Services.Infrastructure.AssemblyReference.Assembly;
    private readonly Assembly _applicationAssembly = MaaldoCom.Services.Application.AssemblyReference.Assembly;
    private readonly Assembly _domainAssembly = MaaldoCom.Services.Domain.AssemblyReference.Assembly;
    private readonly Assembly _apiAssembly = MaaldoCom.Services.Api.AssemblyReference.Assembly;
    private readonly Assembly _cliAssembly = MaaldoCom.Services.Cli.AssemblyReference.Assembly;

    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        var result = Types.InAssembly(_domainAssembly)
            .Should()
            .NotHaveDependencyOn("Application")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(_domainAssembly)
            .Should()
            .NotHaveDependencyOn(_infrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(_domainAssembly)
            .Should()
            .NotHaveDependencyOn(_apiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(_domainAssembly)
            .Should()
            .NotHaveDependencyOn(_cliAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(_applicationAssembly)
            .Should()
            .NotHaveDependencyOn(_infrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(_applicationAssembly)
            .Should()
            .NotHaveDependencyOn(_apiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(_applicationAssembly)
            .Should()
            .NotHaveDependencyOn(_cliAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        var result = Types.InAssembly(_infrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(_apiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_CliLayer()
    {
        var result = Types.InAssembly(_infrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(_cliAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
