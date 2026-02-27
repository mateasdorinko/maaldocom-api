namespace MaaldoCom.Api.Domain.Extensions;

public static class SecurityExtensions
{
    extension(ClaimsPrincipal user)
    {
        public string GetUserId()
        {
            var identityClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return identityClaim?.Value ?? "guest";
        }
    }
}
