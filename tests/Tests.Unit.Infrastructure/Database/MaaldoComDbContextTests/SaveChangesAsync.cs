namespace Tests.Unit.Infrastructure.Database.MaaldoComDbContextTests;

public sealed class SaveChangesAsync : IDisposable
{
    private readonly MaaldoComDbContext _sut;
    private readonly ClaimsPrincipal _user;

    public SaveChangesAsync()
    {
        _sut = DbContextFactory.CreateInMemory();
        _user = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "user-123")]));
    }

    public void Dispose() => _sut.Dispose();

    [Fact]
    public async Task SaveChangesAsync_AddedEntity_SetsAuditFields()
    {
        var album = new MediaAlbum { Name = "Test", UrlFriendlyName = "test" };
        _sut.MediaAlbums.Add(album);

        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        album.CreatedBy.ShouldBe("user-123");
        album.Created.ShouldNotBe(default);
        album.LastModified.ShouldNotBeNull();
        album.LastModifiedBy.ShouldBe("user-123");
    }

    [Fact]
    public async Task SaveChangesAsync_AddedEntityWithPresetCreated_PreservesCreatedDate()
    {
        var presetDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var album = new MediaAlbum { Name = "Test", UrlFriendlyName = "test", Created = presetDate };
        _sut.MediaAlbums.Add(album);

        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        album.Created.ShouldBe(presetDate);
    }

    [Fact]
    public async Task SaveChangesAsync_ModifiedEntityWithAudit_UpdatesLastModified()
    {
        var album = new MediaAlbum { Name = "Test", UrlFriendlyName = "test" };
        _sut.MediaAlbums.Add(album);
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        album.Name = "Updated";
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        album.LastModifiedBy.ShouldBe("user-123");
        album.LastModified.ShouldNotBeNull();
    }

    [Fact]
    public async Task SaveChangesAsync_ModifiedEntityWithoutAudit_SkipsLastModifiedUpdate()
    {
        var album = new MediaAlbum { Name = "Test", UrlFriendlyName = "test" };
        _sut.MediaAlbums.Add(album);
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        var savedLastModified = album.LastModified;
        album.Name = "Updated";
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken, audit: false);

        album.LastModified.ShouldBe(savedLastModified);
    }

    [Fact]
    public async Task SaveChangesAsync_DeletedEntity_SoftDeletesEntity()
    {
        var album = new MediaAlbum { Name = "Test", UrlFriendlyName = "test" };
        _sut.MediaAlbums.Add(album);
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        _sut.MediaAlbums.Remove(album);
        await _sut.SaveChangesAsync(_user, TestContext.Current.CancellationToken);

        var dbAlbum = await _sut.MediaAlbums.FindAsync([album.Id], TestContext.Current.CancellationToken);
        dbAlbum.ShouldNotBeNull();
    }
}
