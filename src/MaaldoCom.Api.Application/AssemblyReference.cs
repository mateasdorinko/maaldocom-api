using System.Reflection;

namespace MaaldoCom.Api.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}