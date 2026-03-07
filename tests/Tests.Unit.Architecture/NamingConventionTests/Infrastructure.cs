using Microsoft.EntityFrameworkCore;

namespace Tests.Unit.Architecture.NamingConventionTests;

public class Infrastructure : BaseTest
{
    [Fact]
    public void EntityConfigurations_ShouldBeSuffixedWithConfiguration()
    {
        Classes().That()
            .Are(InfrastructureLayer)
            .And()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should().HaveNameEndingWith("Configuration")
            .Check(Architecture);
    }
}
