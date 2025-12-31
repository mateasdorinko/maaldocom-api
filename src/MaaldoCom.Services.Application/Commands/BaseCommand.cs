using System.Security.Claims;
using System.Text.Json.Serialization;
using MaaldoCom.Services.Application.Interfaces;

namespace MaaldoCom.Services.Application.Commands;

public abstract class BaseCommand(ClaimsPrincipal user)
{
    [JsonIgnore]
    public ClaimsPrincipal User { get; } = user;
}

public abstract class BaseCommandHandler
{
    protected BaseCommandHandler(IMaaldoComDbContext maaldoComDbContext)
    {
        MaaldoComDbContext = maaldoComDbContext;

        MaaldoComDbContext.DisableChangeTracking();
    }

    [JsonIgnore]
    protected IMaaldoComDbContext MaaldoComDbContext { get; }
}