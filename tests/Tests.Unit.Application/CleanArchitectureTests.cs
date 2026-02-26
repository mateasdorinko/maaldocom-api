using NetArchTest.Rules;

namespace Tests.Unit.Application;

public class CleanArchitectureTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(MaaldoCom.Api.Domain.AssemblyReference.Assembly)
            .Should()
            .NotHaveDependencyOn(MaaldoCom.Api.Application.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
