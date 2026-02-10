using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using NSwag;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MaaldoCom.Services.Api.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public AuthenticationBuilder AddAuthentication(string auth0Domain, string auth0Audience)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{auth0Domain}/";
                options.Audience = auth0Audience;
            });
        }

        public IServiceCollection AddMaaldoCors()
        {
            var corsOrigins = new[]
            {
                "http://localhost:3000",
                "https://app-maaldocomweb-dev-cus.azurewebsites.net",
                "https://app-maaldocomweb-tst-cus.azurewebsites.net",
                "https://app-maaldocomweb-prd-cus.azurewebsites.net",
                "https://www.maaldo.com",
                "https://maaldo.com"
            };

            return services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins(corsOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public IServiceCollection AddSwaggerDocumentForFastEndpoints(string apiDocTitle, string auth0Domain, string auth0Audience)
        {
            return services.SwaggerDocument(options =>
            {
                options.DocumentSettings = s =>
                {
                    s.Title = apiDocTitle;
                    s.Version = "v1";
                    s.AddSecurity("OAuth2",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.OAuth2,
                            Description = "Auth0 OAuth2 authentication",
                            Flows = new OpenApiOAuthFlows
                            {
                                AuthorizationCode = new OpenApiOAuthFlow
                                {
                                    AuthorizationUrl = $"https://{auth0Domain}/authorize?audience={auth0Audience}",
                                    TokenUrl = $"https://{auth0Domain}/oauth/token",
                                    Scopes = new Dictionary<string, string> { { "openid", "OpenID Connect" }, { "profile", "User profile" } }
                                }
                            }
                        });
                };
                options.ShortSchemaNames = true;
            });
        }

        public IServiceCollection ConfigureForwardedHeaders()
        {
            return services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownIPNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }

        public OpenTelemetryBuilder AddOtel(WebApplicationBuilder builder, string? otelEndpoint, string? otelHeaders, bool debugOtelConsoleExporter)
        {
            if (string.IsNullOrEmpty(otelEndpoint)) { return null!; }

            // Force explicit bucket histograms; Grafana Cloud does not support ExponentialHistogram
            Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_METRICS_DEFAULT_HISTOGRAM_AGGREGATION", "explicit_bucket_histogram");

            Action<OtlpExporterOptions> OtlpExporterOptions(string signalPath) => options =>
            {
                options.Endpoint = new Uri($"{otelEndpoint}{signalPath}");
                options.Protocol = OtlpExportProtocol.HttpProtobuf;
                options.Headers = otelHeaders;
            };

            return builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource
                        .AddService("maaldo-com-api")
                        .AddAttributes(new List<KeyValuePair<string, object>>
                        {
                            new("deployment.environment", builder.Environment.EnvironmentName),
                            new("service.namespace", "maaldo-com")
                        });
                })
                .WithMetrics(metrics =>
                {
                    metrics.AddAspNetCoreInstrumentation();
                    metrics.AddHttpClientInstrumentation();
                    metrics.AddOtlpExporter((exporterOptions, metricReaderOptions) =>
                    {
                        OtlpExporterOptions("/v1/metrics")(exporterOptions);
                        metricReaderOptions.TemporalityPreference = MetricReaderTemporalityPreference.Cumulative;
                    });
                })
                .WithTracing(tracing =>
                {
                    tracing.AddHttpClientInstrumentation();
                    tracing.AddAspNetCoreInstrumentation();
                    //tracing.AddEntityFrameworkCoreInstrumentation();
                    //tracing.AddSource();
                    tracing.AddOtlpExporter(OtlpExporterOptions("/v1/traces"));
                })
                .WithLogging(logging =>
                {
                    logging.AddOtlpExporter(OtlpExporterOptions("/v1/logs"));

                    if (debugOtelConsoleExporter)
                    {
                        builder.Logging.ClearProviders();
                        logging.AddConsoleExporter();
                    }
                }, options =>
                {
                    options.IncludeScopes = true;
                    options.IncludeFormattedMessage = true;
                });
        }
    }
}
