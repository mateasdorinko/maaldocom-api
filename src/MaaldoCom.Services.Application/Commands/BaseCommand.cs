namespace MaaldoCom.Services.Application.Commands;

public abstract class BaseCommand(ClaimsPrincipal user)
{
    public ClaimsPrincipal User { get; } = user;
}

public abstract class BaseCommandHandler
{
    protected BaseCommandHandler(IMaaldoComDbContext maaldoComDbContext)
    {
        MaaldoComDbContext = maaldoComDbContext;

        MaaldoComDbContext.EnableChangeTracking();
    }

    protected IMaaldoComDbContext MaaldoComDbContext { get; }
}