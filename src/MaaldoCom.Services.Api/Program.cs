using MaaldoCom.Services.Application.Messaging;
using MaaldoCom.Services.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
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

