using MaaldoCom.Api.Application.Messaging;
using Type = System.Type;
using Shouldly;

namespace Tests.Unit.Architecture.CoLocationTests;

public class Application : BaseTest
{
    [Theory]
    [MemberData(nameof(GetHandlerAndCommandPairs))]
    public void Handlers_ShouldResideInSameNamespaceAsTheirCommandOrQuery(Type handlerType, Type commandOrQueryType)
    {
        handlerType.Namespace.ShouldBe(
            commandOrQueryType.Namespace,
            $"{handlerType.Name} should be in the same namespace as {commandOrQueryType.Name}");
    }

    public static TheoryData<Type, Type> GetHandlerAndCommandPairs()
    {
        Type[] handlerInterfaces =
        [
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>),
            typeof(IQueryHandler<,>)
        ];

        var pairs = new TheoryData<Type, Type>();

        IEnumerable<Type> handlers = MaaldoCom.Api.Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false })
            .Where(t => t.DeclaringType is null);

        foreach (Type handler in handlers)
        {
            foreach (Type iface in handler.GetInterfaces())
            {
                if (!iface.IsGenericType)
                {
                    continue;
                }

                Type genericDef = iface.GetGenericTypeDefinition();

                if (!handlerInterfaces.Contains(genericDef))
                {
                    continue;
                }

                Type commandOrQueryType = iface.GetGenericArguments()[0];
                pairs.Add(handler, commandOrQueryType);
            }
        }

        return pairs;
    }
}
