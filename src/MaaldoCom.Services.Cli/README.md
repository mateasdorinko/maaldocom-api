# MaaldoCom.Services.Cli

## CLI Setup

These commands are intended to be run from the `MaaldoCom.Services.Cli` project directory.

### OpenAPI Document Specification Generation

Run the following command to install the Refitter tool globally:

```shell
dotnet tool install --global Refitter
```

To generate the proxy client code from the OpenAPI document with Refitter, use the following command:

```shell
Refitter --url https://app-maaldocomapi-tst-cus.azurewebsites.net/openapi/v1.json --output ./IApiClient.cs --namespace MaaldoCom.Services.Cli
```


