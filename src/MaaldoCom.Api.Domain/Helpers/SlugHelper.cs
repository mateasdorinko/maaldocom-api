namespace MaaldoCom.Api.Domain.Helpers;

public static class SlugHelper
{
    public static string GetProperNameFromSlug(string slug)
    {
        var parts = slug.Split(['-'], StringSplitOptions.RemoveEmptyEntries);

        var words = parts.Select(p =>
        {
            var lower = p.ToLowerInvariant();
            return char.ToUpperInvariant(lower[0]) + (lower.Length > 1 ? lower.Substring(1) : string.Empty);
        });

        return string.Join(" ", words);
    }

    public static string GetSlugFromProperName(string name)
    {
        return name
            .Replace('_', '-')
            .Replace(' ', '-')
            .ToLowerInvariant();
    }
}
