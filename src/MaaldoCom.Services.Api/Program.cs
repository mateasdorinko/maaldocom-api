using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
//using MaaldoCom.Services.Application.Messaging;
using MaaldoCom.Services.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi;
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
    .AddOpenApi(options =>
    {
        options.AddDocumentTransformer((document, _, _) =>
        {
            document.Info = new OpenApiInfo
            {
                Title = apiDocTitle,
                Version = "1.0.0"
            };
            return Task.CompletedTask;
        });
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
    })
    .Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownIPNetworks.Clear();
        options.KnownProxies.Clear();
    })
    .AddInfrastructureServices(builder.Configuration);

//builder.Services.AddMediator();

var app = builder.Build();
app.UseResponseCaching()
    .UseHsts()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseDefaultExceptionHandler()
    .UseFastEndpoints();

app.MapOpenApi();
app.UseForwardedHeaders();
app.MapScalarApiReference("/docs", options =>
{
    options.WithTitle(apiDocTitle);
    options.OperationTitleSource = OperationTitleSource.Path;
    options.ShowOperationId();
});

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

await app.RunAsync();

