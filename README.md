<img src="/assets/logo.svg" alt="logo" width="100" />

# MaaldoCom Api

**REST API backend for maaldo.com, built on Clean Architecture with .NET 10.**

[![CI/CD Pipeline](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/ci-cd.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mateasdorinko_maaldocom-services&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mateasdorinko_maaldocom-services)
![Code Coverage](https://raw.githubusercontent.com/mateasdorinko/maaldocom-api/badges/badges/badge_branchcoverage.svg)
[![Deploy to Test](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-test.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-test.yml)
[![Deploy to Production](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-prod.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-prod.yml)

## Table of Contents

- [Overview](#overview)
- [Purpose](#purpose)
- [Architecture Overview](#architecture-overview)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Testing Strategy](#testing-strategy)
- [Development Conventions](#development-conventions)
- [Related Project Documentation](#related-project-documentation)

## Overview

MaaldoCom.Api is an ASP.NET Core REST API built with FastEndpoints and structured as a Clean Architecture solution across four source projects. Business rules live in the Domain and Application layers; Infrastructure satisfies their contracts via EF Core, FusionCache, Azure Blob Storage, and Mailgun; the Api layer exposes HTTP endpoints with JWT authentication via Auth0. A custom mediator pattern with decorator pipelines handles validation and structured logging for all use cases. OpenTelemetry traces and metrics are exported to Grafana Cloud.

## Purpose

Serve as the back-end for maaldo.com — a personal website. Exposes REST endpoints for knowledge entries, media albums, tags, and system operations such as cache refresh and email dispatch.

## Architecture Overview

### Clean Architecture

Inner layers define abstractions; outer layers provide implementations. No inner layer depends on an outer layer. Business logic is isolated from infrastructure and HTTP concerns.

### Dependency Direction

```
Domain (no deps) ← Application ← Infrastructure ← Api
```

Each project references only the layer directly inside it. The arrow represents a dependency — Application depends on Domain, and so on.

### Solution Structure

| Project | Layer | Role |
|---|---|---|
| `MaaldoCom.Api.Domain` | Domain | Entities and base types |
| `MaaldoCom.Api.Application` | Application | Use cases, interfaces, DTOs, decorators |
| `MaaldoCom.Api.Infrastructure` | Infrastructure | EF Core, caching, blob storage, email |
| `MaaldoCom.Api` | Presentation | FastEndpoints, HTTP, API docs |

### Repository Structure

```
src/
  MaaldoCom.Api/                  # Presentation layer
  MaaldoCom.Api.Application/      # Application layer
  MaaldoCom.Api.Domain/           # Domain layer
  MaaldoCom.Api.Infrastructure/   # Infrastructure layer
tests/
  Tests.Unit.Api/
  Tests.Unit.Application/
  Tests.Unit.Domain/
  Tests.Unit.Infrastructure/
  Tests.Unit.Architecture/
  Tests.Integration/
```

## Technology Stack

| Concern | Technology |
|---|---|
| Framework | ASP.NET Core / FastEndpoints |
| Language | C# / .NET 10 |
| Database | SQL Server (EF Core) |
| Caching | FusionCache (L1 in-memory + L2 distributed) |
| Storage | Azure Blob Storage / Azurite (local) |
| Email | Mailgun |
| Authentication | Auth0 (JWT Bearer / OpenID Connect) |
| API Docs | Scalar |
| Telemetry | OpenTelemetry → Grafana Cloud (OTLP) |
| Testing | xUnit v3, Shouldly, FakeItEasy, ArchUnitNET |
| Code Quality | SonarCloud, SonarAnalyzer |
| CI/CD | GitHub Actions |
| Hosting | Azure App Service |
| Containerization | Docker (multi-stage) |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://docs.docker.com/get-docker/) — for SQL Server and Azurite
- [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) — `dotnet tool install --global dotnet-ef`

### Local Services (Docker Compose)

Start SQL Server and Azurite locally before running the API. Create the files below and replace `MY_SUPER_SECRET_PASSWORD` in the `.env` file.

**SQL Server**

_docker-compose.yml_
```yaml
services:
  sqlserver-2022:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver-2022
    environment:
      ACCEPT_EULA: Y
      MSSQL_SA_PASSWORD: ${MSSQL_SA_PASSWORD}
      MSSQL_PID: Developer
    ports:
      - "1433:1433"
    volumes:
      - data:/var/opt/mssql
    restart: unless-stopped

volumes:
  data:
```

_.env_
```
MSSQL_SA_PASSWORD="MY_SUPER_SECRET_PASSWORD"
```

**Azurite (Azure Storage Emulator)**

_docker-compose.yml_
```yaml
services:
  azurite-emulator:
    image: mcr.microsoft.com/azure-storage/azurite
    container_name: azurite-emulator
    restart: unless-stopped
    command: "azurite --blobHost 0.0.0.0 --queueHost 0.0.0.0 --tableHost 0.0.0.0 --location /data --debug /data/debug.log --skipApiVersionCheck --loose"
    ports:
      - "10000:10000"
      - "10001:10001"
      - "10002:10002"
    volumes:
      - data:/data

volumes:
  data:
```

### Restore

```shell
dotnet restore MaaldoCom.Api.sln
```

### Build

```shell
dotnet build MaaldoCom.Api.sln
```

### Run

```shell
dotnet run --project src/MaaldoCom.Api/MaaldoCom.Api.csproj
```

API documentation is available at `/docs` after startup.

### Test

```shell
dotnet test MaaldoCom.Api.sln
```

## Configuration

Secrets are managed via .NET User Secrets for local development and Azure Key Vault in deployed environments. `AzureKeyVaultUri` must be set in `appsettings.json` or as an environment variable to enable Key Vault configuration at runtime.

Run the following from `src/MaaldoCom.Api/`. The `init` command only needs to be run once per machine:

```shell
dotnet user-secrets init
```

Then set each key:

```shell
dotnet user-secrets set "auth0-audience" "SECRET_VALUE"
dotnet user-secrets set "auth0-domain" "SECRET_VALUE"
dotnet user-secrets set "azure-storage-account-connection-string" "SECRET_VALUE"
dotnet user-secrets set "grafana-cloud-otel-exporter-otlp-endpoint" "SECRET_VALUE"
dotnet user-secrets set "grafana-cloud-otel-exporter-otlp-headers" "SECRET_VALUE"
dotnet user-secrets set "maaldocom-db-connection-string-api-user" "SECRET_VALUE"
dotnet user-secrets set "maaldocom-db-connection-string-migrations-user" "SECRET_VALUE"
dotnet user-secrets set "mailgun-api-key" "SECRET_VALUE"
dotnet user-secrets set "mailgun-base-url" "SECRET_VALUE"
dotnet user-secrets set "mailgun-default-from-email" "SECRET_VALUE"
dotnet user-secrets set "mailgun-default-to-email" "SECRET_VALUE"
dotnet user-secrets set "mailgun-domain" "SECRET_VALUE"
dotnet user-secrets set "scalar-client-id" "SECRET_VALUE"
```

## Testing Strategy

Tests are organized into unit, architecture, and integration suites. Each source project has a corresponding unit test project mirroring its namespace structure.

- **Unit tests** (xUnit v3, Shouldly, FakeItEasy) — fully isolated, no I/O. Handlers tested with faked dependencies; endpoints tested with `Factory.Create<TEndpoint>`.
- **Architecture tests** (ArchUnitNET) — reflect on compiled assemblies to enforce layer dependency rules, naming conventions, visibility, and co-location. Run in CI and fail the build on violations.
- **Integration tests** (FastEndpoints.Testing) — boot the full application via `AppFixture` and exercise the HTTP pipeline end-to-end.

## Development Conventions

- **Mediator pattern**: dispatch via `IQueryHandler<TQuery, TResponse>` or `ICommandHandler<TCommand, TResponse>`. Commands take a `ClaimsPrincipal`. Handlers are auto-registered via Scrutor.
- **Decorator pipeline**: validation runs before all command handlers; structured logging wraps all handler executions.
- **Results**: all handler results are wrapped in `FluentResults.Result<T>`. Endpoints use `result.Match(onSuccess, onFailure)` to map to HTTP status codes.
- **Naming**: types must end with `Command`, `CommandHandler`, `Query`, `QueryHandler`, or `Validator` as appropriate. Enforced by architecture tests.
- **Endpoints**: override `Configure()` for route and auth, `HandleAsync()` for logic. Response models extend `BaseModel` for HATEOAS `href` support.
- **Caching**: always use `ICacheManager`; never cache directly. Default TTL is 20 minutes.
- **Warnings as errors**: `Directory.Build.props` enables `TreatWarningsAsErrors`, nullable reference types, and SonarAnalyzer on all projects.

## Related Project Documentation

| Src Project | Readme | Test Project | Readme |
|---|---|---|---|
| `MaaldoCom.Api` | [README](src/MaaldoCom.Api/README.md) | `Tests.Unit.Api` | [README](tests/Tests.Unit.Api/README.md) |
| `MaaldoCom.Api.Application` | [README](src/MaaldoCom.Api.Application/README.md) | `Tests.Unit.Application` | [README](tests/Tests.Unit.Application/README.md) |
| `MaaldoCom.Api.Domain` | [README](src/MaaldoCom.Api.Domain/README.md) | `Tests.Unit.Domain` | [README](tests/Tests.Unit.Domain/README.md) |
| `MaaldoCom.Api.Infrastructure` | [README](src/MaaldoCom.Api.Infrastructure/README.md) | `Tests.Unit.Infrastructure` | [README](tests/Tests.Unit.Infrastructure/README.md) |
| | | `Tests.Unit.Architecture` | [README](tests/Tests.Unit.Architecture/README.md) |
| | | `Tests.Integration` | [README](tests/Tests.Integration/README.md) |
