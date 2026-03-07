using MaaldoCom.Api.Application.Messaging;

namespace Tests.Unit.Architecture.VisibilityTests;

public class Application : BaseTest
{
    [Fact]
    public void Commands_ShouldBePublicRecords()
    {
        Classes().That()
            .Are(ApplicationLayer)
            .And()
            .ImplementInterface(typeof(ICommand<>))
            .Should().BePublic()
            .AndShould().BeRecord()
            .Check(Architecture);
    }

    [Fact]
    public void Queries_ShouldBePublicRecords()
    {
        Classes().That()
            .Are(ApplicationLayer)
            .And()
            .ImplementInterface(typeof(IQuery<>))
            .Should().BePublic()
            .AndShould().BeRecord()
            .Check(Architecture);
    }

    [Fact]
    public void CommandHandlers_ShouldNotBePublic()
    {
        Classes().That()
            .Are(ApplicationLayer)
            .And()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should().BeInternal()
            .Check(Architecture);
    }

    [Fact]
    public void QueryHandlers_ShouldNotBePublic()
    {
        Classes().That()
            .Are(ApplicationLayer)
            .And()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should().BeInternal()
            .Check(Architecture);
    }
}
