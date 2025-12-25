using System.Reflection;

namespace MaaldoCom.Services.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Application.AssemblyReference).Assembly;
}