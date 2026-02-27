using System.Security.Claims;
using MaaldoCom.Api.Domain.Extensions;

namespace Tests.Unit.Domain.Extensions.SecurityExtensionsTests;

public class GetUserId
{
    [Fact]
    public void GetUserId_HasNameIdentifier_ReturnsUserId()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        A.CallTo(() => user.Claims).Returns(new List<Claim> { new (ClaimTypes.NameIdentifier, "maaldo") });

        // act
        var result = user.GetUserId();

        // assert
        result.ShouldBe("maaldo");
    }

    [Fact]
    public void GetUserId_NoClaims_ReturnsGuest()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();

        // act
        var result = user.GetUserId();

        // assert
        result.ShouldBe("guest");
    }

    [Fact]
    public void GetUserId_HasClaimsButNoNameIdentifier_ReturnsGuest()
    {
        // arrange
        var user = A.Fake<ClaimsPrincipal>();
        A.CallTo(() => user.Claims).Returns(new List<Claim> { new ("asdf", "qwer") });

        // act
        var result = user.GetUserId();

        // assert
        result.ShouldBe("guest");
    }
}
