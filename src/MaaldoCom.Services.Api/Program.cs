using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FastEndpoints.Swagger;
using MaaldoCom.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using NSwag;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

const string apiDocTitle = "maaldo.com API Reference";
var auth0Domain = builder.Configuration["auth0-domain"]!;
var auth0Audience = builder.Configuration["auth0-audience"]!;
var auth0ClientId = builder.Configuration["scalar-client-id"]!;
var keyVaultUri = builder.Configuration["AzureKeyVaultUri"]!;
var otelEndpoint = builder.Configuration["grafana-cloud-otel-exporter-otlp-endpoint"];
var otelHeaders = builder.Configuration["grafana-cloud-otel-exporter-otlp-headers"];

builder.Logging.ClearProviders();

if (!string.IsNullOrEmpty(keyVaultUri))
{
    var credential = new DefaultAzureCredential();
    var secretClient = new SecretClient(new Uri(keyVaultUri), credential);

    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{auth0Domain}/";
        options.Audience = auth0Audience;
    });

builder.Services
    .AddAuthorization()
    .AddFastEndpoints(options => { options.Assemblies = [MaaldoCom.Services.Application.AssemblyReference.Assembly]; })
    .AddResponseCaching()
    .SwaggerDocument(options =>
    {
        options.DocumentSettings = s =>
        {
            s.Title = apiDocTitle;
            s.Version = "v1";
            s.AddSecurity("OAuth2", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Description = "Auth0 OAuth2 authentication",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = $"https://{auth0Domain}/authorize?audience={auth0Audience}",
                        TokenUrl = $"https://{auth0Domain}/oauth/token",
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect" },
                            { "profile", "User profile" }
                        }
                    }
                }
            });
        };
        options.ShortSchemaNames = true;
    })
    .Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownIPNetworks.Clear();
        options.KnownProxies.Clear();
    })
    .AddInfrastructureServices(builder.Configuration);

if (!string.IsNullOrEmpty(otelEndpoint))
{
    Action<OtlpExporterOptions> otlpExporterOptions = options =>
    {
        options.Endpoint = new Uri(otelEndpoint);
        options.Protocol = OtlpExportProtocol.HttpProtobuf;
        options.Headers = otelHeaders;
    };

    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource =>
        {
            resource
                .AddService("maaldo-com-api")
                .AddAttributes(new List<KeyValuePair<string, object>>
                {
                    new ("deployment.environment", builder.Environment.EnvironmentName),
                    new ("service.namespace", "maaldo-com")
                });
        })
        .WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddHttpClientInstrumentation();
            metrics.AddOtlpExporter(otlpExporterOptions);
        })
        .WithTracing(tracing =>
        {
            tracing.AddHttpClientInstrumentation();
            tracing.AddAspNetCoreInstrumentation();
            //tracing.AddEntityFrameworkCoreInstrumentation();
            //tracing.AddSource();
            tracing.AddOtlpExporter(otlpExporterOptions);
        })
        .WithLogging(logging =>
        {
            logging.AddOtlpExporter(otlpExporterOptions);

            if (builder.Environment.IsDevelopment()) { logging.AddConsoleExporter(); }
        }, options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });
}

var app = builder.Build();

app.UseResponseCaching()
    .UseHsts()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseDefaultExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseForwardedHeaders();

app.MapScalarApiReference("/docs", options =>
{
    options.WithTitle(apiDocTitle);
    options.WithFavicon("/favicon.ico");
    options.OperationTitleSource = OperationTitleSource.Path;
    options.ShowOperationId();
    options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    options.AddPreferredSecuritySchemes("OAuth2");
    options.AddAuthorizationCodeFlow("OAuth2", flow =>
    {
        flow.ClientId = auth0ClientId;
        flow.Pkce = Pkce.Sha256;
        flow.WithCredentialsLocation(CredentialsLocation.Body);
        flow.SelectedScopes = ["openid", "profile"];
        flow.AdditionalQueryParameters = new Dictionary<string, string> { { "audience", auth0Audience } };
        flow.AdditionalBodyParameters = new Dictionary<string, string> { { "audience", auth0Audience } };
    });
});

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

await app.RunAsync();
