using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FastEndpoints.Swagger;
using MaaldoCom.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using NSwag;
using Scalar.AspNetCore;

const string apiDocTitle = "maaldo.com API Reference";

var builder = WebApplication.CreateBuilder(args);

var keyVaultUri = builder.Configuration["AzureKeyVaultUri"];

if (!string.IsNullOrEmpty(keyVaultUri))
{
    var credential = new DefaultAzureCredential();
    var secretClient = new SecretClient(new Uri(keyVaultUri), credential);

    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

var auth0Domain = builder.Configuration["auth0-domain"]!;
var auth0Audience = builder.Configuration["auth0-audience"]!;
var auth0ClientId = builder.Configuration["scalar-client-id"]!;

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
    .AddFastEndpoints(options =>
    {
        options.Assemblies = [MaaldoCom.Services.Application.AssemblyReference.Assembly];
    })
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
                            { "profile", "User profile" },
                            { "write:media-albums", "Create media albums" }
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

var app = builder.Build();
app.UseResponseCaching()
    .UseHsts()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseDefaultExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints();

app.UseSwaggerGen();
app.UseForwardedHeaders();
app.MapScalarApiReference("/docs", options =>
{
    options.WithTitle(apiDocTitle);
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
        flow.AdditionalQueryParameters = new Dictionary<string, string>
        {
            { "audience", auth0Audience }
        };
        flow.AdditionalBodyParameters = new Dictionary<string, string>
        {
            { "audience", auth0Audience }
        };
    });
});

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

await app.RunAsync();
