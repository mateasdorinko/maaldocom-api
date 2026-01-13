# MaaldoCom.Services.Infrastructure

## Setup

### Entity Framework

#### Migration Requisites

Before creating or applying migrations, ensure that you have the following prerequisites installed:

```shell
dotnet tool install --global dotnet-ef
```

#### Migration Commands

##### Adding a Migration

Migration commands must be executed in the `MaaldoCom.Services.Infrastructure` project, while the startup project is 
set to `MaaldoCom.Services.Api`. This ensures that the correct configuration and dependencies are used.

```shell
dotnet ef migrations add [MIGRATION_NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```

##### Applying Migrations

```shell
dotnet ef database update --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```

##### Removing a Migration

```shell
dotnet ef migrations remove --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```
