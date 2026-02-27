using MaaldoCom.Api.Domain.Entities;
using System.Security.Claims;

namespace Tests.Unit.Domain.Entities.ExtensionsTests;

public class InitializeForCreate
{
    [Fact]
    public void InitializeForCreate_EmptyCreatedDate_SetsUtcNow()
    {
        // Arrange
        var entity = new MediaAlbum();
        var user = A.Fake<ClaimsPrincipal>();
        A.CallTo(() => user.Claims).Returns(new List<Claim> { new (ClaimTypes.NameIdentifier, "maaldo") });

        // Act
        entity.InitializeForCreate(user);

        // Assert
        entity.Created.ShouldBeGreaterThan(DateTime.MinValue);
        entity.Created.ShouldBeLessThan(DateTime.MaxValue);
        entity.Active.ShouldBeTrue();
        entity.CreatedBy.ShouldBe("maaldo");
        entity.LastModifiedBy.ShouldBe("maaldo");
    }

    [Fact]
    public void InitializeForCreate_ProvidedCreatedDate_SetsUtcNow()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var entity = new MediaAlbum { Created = now };
        var user = A.Fake<ClaimsPrincipal>();
        A.CallTo(() => user.Claims).Returns(new List<Claim> { new (ClaimTypes.NameIdentifier, "maaldo") });

        // Act
        entity.InitializeForCreate(user);

        // Assert
        entity.Created.ShouldBe(now);
        entity.Active.ShouldBeTrue();
        entity.CreatedBy.ShouldBe("maaldo");
        entity.LastModifiedBy.ShouldBe("maaldo");
    }
}
