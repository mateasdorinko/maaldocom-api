using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
//using MaaldoCom.Services.Application.Messaging;
using MaaldoCom.Services.Infrastructure;
using Scalar.AspNetCore;

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
    .AddOpenApi()
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
app.MapScalarApiReference("/docs", options => { options.WithTitle("maaldo.com API Reference"); });

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

await app.RunAsync();

