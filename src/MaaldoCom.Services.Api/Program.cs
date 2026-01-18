using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FastEndpoints.Swagger;
using MaaldoCom.Services.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
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

builder.Services
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
    .UseFastEndpoints();

app.UseSwaggerGen();
app.UseForwardedHeaders();
app.MapScalarApiReference("/docs", options =>
{
    options.WithTitle(apiDocTitle);
    options.OperationTitleSource = OperationTitleSource.Path;
    options.ShowOperationId();
    options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
});

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

await app.RunAsync();

