# MaaldoCom.Services.Infrastructure

## Entity Framework

### Migration Requisites

Before creating or applying migrations, ensure that you have the following prerequisites installed:

```
dotnet tool install --global dotnet-ef
```

### Create Migration

```
dotnet ef migrations add [MIGRATION_NAME] --output-dir Database/Migrations --startup-project MaaldoCom.Services.Api
```