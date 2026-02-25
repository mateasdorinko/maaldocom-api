using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FastEndpoints.Swagger;
using MaaldoCom.Services.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.SetupDevEnvironmentOnlyConfig();

const string apiDocTitle = "maaldo.com API Reference";

var keyVaultUri = builder.Configuration["AzureKeyVaultUri"]!;
if (!string.IsNullOrEmpty(keyVaultUri))
{
    var credential = new DefaultAzureCredential();
    var secretClient = new SecretClient(new Uri(keyVaultUri), credential);

    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

var auth0Domain = builder.Configuration["auth0-domain"]!;
var auth0Audience = builder.Configuration["auth0-audience"]!;
var auth0ClientId = builder.Configuration["scalar-client-id"]!;
var otelEndpoint = builder.Configuration["grafana-cloud-otel-exporter-otlp-endpoint"];
var otelHeaders = builder.Configuration["grafana-cloud-otel-exporter-otlp-headers"];

builder.Services.AddAuthentication(auth0Domain, auth0Audience);

builder.Services
    .AddAuthorization()
    .AddFastEndpoints(options => { options.Assemblies = [MaaldoCom.Services.Application.AssemblyReference.Assembly]; })
    .AddResponseCaching()
    .AddSwaggerDocumentForFastEndpoints(apiDocTitle, auth0Domain, auth0Audience)
    .ConfigureForwardedHeaders()
    .AddMaaldoCors()
    .AddInfrastructureServices(builder.Configuration)
    .AddMediator(MaaldoCom.Services.Application.AssemblyReference.Assembly);

builder.Services.AddOtel(builder, otelEndpoint, otelHeaders, false);

var app = builder.Build();

app.UseCors()
    .UseResponseCaching()
    .UseHsts()
    .UseStaticFiles()
    .UseDefaultExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseForwardedHeaders();

app.UseScalar(apiDocTitle, auth0ClientId, auth0Audience);
app.UseDevEnvironmentOnlyMiddleware();

await app.RunAsync();
