using Scalar.AspNetCore;

namespace MaaldoCom.Api.Extensions;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public IEndpointConventionBuilder UseScalar(string apiDocTitle, string auth0ClientId, string auth0Audience)
        {
            return app.MapScalarApiReference("/docs", options =>
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
        }

        public IApplicationBuilder UseDevEnvironmentOnlyMiddleware()
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }

        public IApplicationBuilder UseProdEnvironmentOnlyMiddleware()
        {
            // if (app.Environment.IsProduction())
            // {
            //
            // }

            return app;
        }
    }
}
