using FluentValidation;
using MaaldoCom.Api.Application.Messaging;

namespace Tests.Unit.Architecture.NamingConventionTests;

public class Application : BaseTest
{
    [Fact]
    public void Commands_ShouldBeSuffixedWithCommand()
    {
        Classes().That()
            .ImplementInterface(typeof(ICommand))
            .Should().HaveNameEndingWith("Command")
            .Check(Architecture);
    }

    [Fact]
    public void CommandHandlers_ShouldBeSuffixedWithCommandHandler()
    {
        Classes().That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .And().DoNotResideInNamespace("MaaldoCom.Api.Application.Messaging.Behaviors")
            .Should().HaveNameEndingWith("CommandHandler")
            .Check(Architecture);
    }

    [Fact]
    public void Queries_ShouldBeSuffixedWithQuery()
    {
        Classes().That()
            .ImplementInterface(typeof(IQuery<>))
            .Should().HaveNameEndingWith("Query")
            .Check(Architecture);
    }

    [Fact]
    public void QueryHandlers_ShouldBeSuffixedWithQueryHandler()
    {
        Classes().That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .And().DoNotResideInNamespace("MaaldoCom.Api.Application.Messaging.Behaviors")
            .Should().HaveNameEndingWith("QueryHandler")
            .Check(Architecture);
    }

    [Fact(Skip = "Need to find how to detect objects that inherit from AbstractValidator")]
    public void Validators_ShouldBeSuffixedWithValidator()
    {
        Classes().That()
            .ImplementInterface(typeof(IValidator<>))
            .Or()
            .ImplementInterface(typeof(AbstractValidator<>))
            .Should().HaveNameEndingWith("Validator")
            .Check(Architecture);
    }
}
