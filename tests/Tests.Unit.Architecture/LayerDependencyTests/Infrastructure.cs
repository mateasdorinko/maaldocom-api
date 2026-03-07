namespace Tests.Unit.Architecture.LayerDependencyTests;

public class Infrastructure : BaseTest
{
    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        Types().That().Are(InfrastructureLayer).Should()
            .NotDependOnAny(ApiLayer)
            .Check(Architecture);
    }
}
