# MaaldoCom.Services.Infrastructure

## Entity Framework

### Migration Requisites

Before creating or applying migrations, ensure that you have the following prerequisites installed:

```
dotnet tool install --global dotnet-ef
```

### Create Migration

Migrations must be created in the `MaaldoCom.Services.Infrastructure` project, but the startup project should be set to `MaaldoCom.Services.Api`. This ensures that the correct configuration and dependencies are used.

```
dotnet ef migrations add [MIGRATION_NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```