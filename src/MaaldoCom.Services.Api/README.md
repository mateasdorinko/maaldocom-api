# MaaldoCom.Services.Api

## Local User Secrets

### Initialization

To initialize user secrets for the `MaaldoCom.Services.Api` project, run the following command in the project directory:

```shell
dotnet user-secrets init
```

This will create a `UserSecretsId` entry in the `.csproj` file that can be used to store sensitive information 
locally. It only needs to be created once, and all other computers only need to run the `set` commands.

### Setting User Secrets

To set user secrets, use the following command format:

```shell
dotnet user-secrets set "Key" "Value" --project MaaldoCom.Services.Api.csproj
```
Create configuration entries for the following keys:

- maaldocom-db-connection-string-api-user


