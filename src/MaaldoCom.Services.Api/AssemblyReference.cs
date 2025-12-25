using System.Reflection;

namespace MaaldoCom.Services.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}