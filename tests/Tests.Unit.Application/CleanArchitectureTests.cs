using NetArchTest.Rules;

namespace Tests.Unit.Application;

public class CleanArchitectureTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Services.Domain.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Services.Application.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
