namespace Tests.Unit.Infrastructure.TestHelpers;

internal static class DbContextFactory
{
    internal static MaaldoComDbContext CreateInMemory(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<MaaldoComDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;

        return new MaaldoComDbContext(options);
    }

    internal static IDbContextFactory<MaaldoComDbContext> CreateFactory(MaaldoComDbContext dbContext)
    {
        var factory = A.Fake<IDbContextFactory<MaaldoComDbContext>>();
        A.CallTo(() => factory.CreateDbContext()).Returns(dbContext);
        return factory;
    }
}
