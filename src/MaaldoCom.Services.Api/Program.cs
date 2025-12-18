using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddFastEndpoints()
    .AddResponseCaching()
    .AddOpenApi();

var app = builder.Build();
app.UseResponseCaching() 
    .UseStaticFiles()
    .UseDefaultExceptionHandler()
    .UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs",
        options =>
        {
            options.WithTitle("maaldo.com API Reference");
        });
}

app.Run();