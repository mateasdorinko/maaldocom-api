# MaaldoCom.Services.Cli

## CLI Setup

### OpenAPI Document Specification Generation

Run the following command to install the Kiota tool globally:

```shell
dotnet tool install --global Microsoft.OpenApi.Kiota
```

To generate the Kiota client code from the OpenAPI document, use the following command:

```shell
kiota generate \
  -l CSharp \
  -d "https://app-maaldocomapi-dev-cus.azurewebsites.net/openapi/v1.json" \
  -c MaaldoApiClient \
  -n MaaldoCom.Services.Cli \
  -o ./MaaldoApiClient \
  --clean-output
```