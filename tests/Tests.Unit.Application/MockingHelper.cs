using FakeItEasy;
using System.Security.Claims;
using MaaldoCom.Services.Application.Interfaces;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Tests.Unit.Application;

public class MockingHelper
{
    public Fake<ClaimsPrincipal> User { get; set; } = new();
    public Fake<ILogger> Logger { get; set; } = new();
    public Fake<IConfiguration> Configuration { get; set; } = new();
    public Fake<IMaaldoComDbContext> MaaldoComDbContext { get; set; } = new();
    public Fake<HybridCache> HybridCache { get; set; } = new();
}