using System.Reflection;

namespace MaaldoCom.Api.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}