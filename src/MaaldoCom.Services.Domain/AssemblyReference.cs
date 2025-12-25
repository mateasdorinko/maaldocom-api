using System.Reflection;

namespace MaaldoCom.Services.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}