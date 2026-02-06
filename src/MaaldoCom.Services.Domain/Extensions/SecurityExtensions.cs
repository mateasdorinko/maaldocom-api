namespace MaaldoCom.Services.Domain.Extensions;

public static class SecurityExtensions
{
    extension(ClaimsPrincipal user)
    {
        public string? GetUserId()
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "guest";

        public IEnumerable<string> GetUserClaims()
        {
            return user?.Claims?.Select(c => c.ToString()) ?? new List<string>();
        }
    }
}
