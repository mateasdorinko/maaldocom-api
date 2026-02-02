<img src="/assets/logo.svg" width="100" />

# MaaldoCom.Services.Api

## API Setup

These commands are intended to be run from the `MaaldoCom.Services.Api` project directory.

### Local User Secrets

### Initialization

To initialize user secrets for the `MaaldoCom.Services.Api` project, run the following command in the project directory:

```shell
dotnet user-secrets init
```

This will create a `UserSecretsId` entry in the `.csproj` file that can be used to store sensitive information 
locally. It only needs to be created once, and all other computers only need to run the `set` commands.

### Setting User Secrets

Create configuration entries for the following keys:

```shell
dotnet user-secrets set "maaldocom-db-connection-string-api-user" "SECRET_VALUE"
dotnet user-secrets set "maaldocom-db-connection-string-migrations-user" "SECRET_VALUE"
dotnet user-secrets set "sendgrid-api-key" "SECRET_VALUE"
dotnet user-secrets set "sendgrid-default-from-email" "SECRET_VALUE"
dotnet user-secrets set "sendgrid-default-to-email" "SECRET_VALUE"
dotnet user-secrets set "azure-storage-account-connection-string" "SECRET_VALUE"
dotnet user-secrets set "scalar-client-id" "SECRET_VALUE"
dotnet user-secrets set "auth0-domain" "SECRET_VALUE"
dotnet user-secrets set "auth0-audience" "SECRET_VALUE"
```

