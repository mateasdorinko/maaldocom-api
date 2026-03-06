namespace Tests.Unit.Architecture;

public abstract class BaseTest
{
    protected static readonly IObjectProvider<IType> DomainLayer =
        Types().That()
            .ResideInAssembly(MaaldoCom.Api.Domain.AssemblyReference.Assembly)
            .As("Domain Layer");
    protected static readonly IObjectProvider<IType> ApplicationLayer =
        Types().That()
            .ResideInAssembly(MaaldoCom.Api.Application.AssemblyReference.Assembly)
            .As("Application Layer");
    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        Types().That()
            .ResideInAssembly(MaaldoCom.Api.Infrastructure.AssemblyReference.Assembly)
            .As("Infrastructure Layer");
    protected static readonly IObjectProvider<IType> ApiLayer =
        Types().That()
            .ResideInAssembly(MaaldoCom.Api.AssemblyReference.Assembly)
            .As("Api Layer");

    protected static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            MaaldoCom.Api.Domain.AssemblyReference.Assembly,
            MaaldoCom.Api.Application.AssemblyReference.Assembly,
            MaaldoCom.Api.Infrastructure.AssemblyReference.Assembly,
            MaaldoCom.Api.AssemblyReference.Assembly)
        .Build();
}
