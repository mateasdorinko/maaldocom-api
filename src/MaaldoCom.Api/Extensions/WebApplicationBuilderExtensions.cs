namespace MaaldoCom.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder SetupDevEnvironmentOnlyConfig()
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Logging.AddConsole();
            }

            return builder;
        }

        public WebApplicationBuilder SetupProdEnvironmentOnlyConfig()
        {
            // if (builder.Environment.IsProduction())
            // {
            //
            // }

            return builder;
        }
    }
}
