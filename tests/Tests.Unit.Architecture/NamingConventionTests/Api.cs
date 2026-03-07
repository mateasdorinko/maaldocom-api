using FastEndpoints;

namespace Tests.Unit.Architecture.NamingConventionTests;

public class Api : BaseTest
{
    [Fact]
    public void Endpoints_ShouldBeSuffixedWithEndpoint()
    {
        Classes().That()
            .Are(ApiLayer)
            .And()
            .AreAssignableTo(typeof(EndpointWithoutRequest))
            .Or()
            .AreAssignableTo(typeof(EndpointWithoutRequest<>))
            .Or()
            .AreAssignableTo(typeof(Endpoint<,>))
            .Should().HaveNameEndingWith("Endpoint")
            .Check(Architecture);
    }
}
