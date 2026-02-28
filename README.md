<img src="assets/logo.svg" alt="logo" width="100" />

# MaaldoCom Api

[![CI/CD Pipeline](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/ci-cd.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mateasdorinko_maaldocom-services&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mateasdorinko_maaldocom-services)
![Code Coverage](https://raw.githubusercontent.com/mateasdorinko/maaldocom-api/badges/badges/badge_branchcoverage.svg)
[![Deploy to Test](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-test.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-test.yml)
[![Deploy to Production](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-prod.yml/badge.svg)](https://github.com/mateasdorinko/maaldocom-api/actions/workflows/deploy-prod.yml)

This repository contains the back-end REST API services for maaldo.com, my personal website. The
solution is structured into multiple projects, each responsible for a specific aspect of the application, following
Clean Architecture principles.

## Tech Stack

- **Framework**: ASP.NET Core Web API
- **Language**: C#
- **Runtime**: .NET 10
- **Testing**: xUnit, Shouldly, FakeItEasy
- **Database**: SQL Server (Docker), Azure Edge SQL (MicroK8s)
- **Storage**: Azure Blob Storage (Azurite for local development)
- **Authentication:** Auth0 (OpenID Connect)
- **Telemetry:** OpenTelemetry with OTLP exporter to Azure Monitor
- **CI/CD**: GitHub Actions
- **Hosting:** Azure App Service
- **Code Quality**: SonarCloud
- **Containerization:** Docker (multi-stage, standalone output)
- **Orchestration**: MicroK8s (in progress)

## Getting Started

Set up the required services below for local development using Docker Compose, including SQL Server and Azurite
(Azure Storage Emulator). Then, configure local user secrets and Entity Framework for database access. Finally, install
FFMpeg for media processing tasks.

### Docker Compose

#### SQL Server

Create both the docker-compose and .env files on your machine. Replace the `MY_SUPER_SECRET_PASSWORD` token with a
strong password in the .env file.

_docker-compose.yml_

```yaml
services:
  sqlserver2022:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022
    environment:
      ACCEPT_EULA: Y
      MSSQL_SA_PASSWORD: ${MSSQL_SA_PASSWORD}
      MSSQL_PID: Developer # Developer edition
    ports:
      - "1433:1433"
    volumes:
      - data:/var/opt/mssql
    restart: unless-stopped

volumes:
  data:
```

_.env_

```yaml
MSSQL_SA_PASSWORD="MY_SUPER_SECRET_PASSWORD"
```

#### Azurite

_docker-compose.yml_

```yaml
services:
  azurite-emulator:
    image: mcr.microsoft.com/azure-storage/azurite
    container_name: azurite-emulator
    restart: unless-stopped
    command: "azurite --blobHost 0.0.0.0 --queueHost 0.0.0.0 --tableHost 0.0.0.0 --location /data --debug /data/debug.log --skipApiVersionCheck --loose"
    ports:
      - "10000:10000" # blob storage service
      - "10001:10001" # queue storage service
      - "10002:10002" # table storage service
    volumes:
      - data:/data

volumes:
  data:
```

### Other Setup

- [Local User Secrets](src/MaaldoCom.Api/README.md#local-user-secrets)
- [Entity Framework](src/MaaldoCom.Api.Infrastructure/README.md#entity-framework)
- [FFMpeg](src/MaaldoCom.Api.Infrastructure/README.md#ffmpeg)


## Projects

| Src Project                                                                | Tst Project                                                            |
|----------------------------------------------------------------------------|------------------------------------------------------------------------|
| [MaaldoCom.Api (Presentation)](src/MaaldoCom.Api/README.md)                | [Tests.Unit.Api](tests/Tests.Unit.Api/README.md)                       |
| [MaaldoCom.Api.Infrastructure](src/MaaldoCom.Api.Infrastructure/README.md) | [Tests.Unit.Infrastructure](tests/Tests.Unit.Infrastructure/README.md) |
| [MaaldoCom.Api.Application](src/MaaldoCom.Api.Application/README.md)       | [Tests.Unit.Application](tests/Tests.Unit.Application/README.md)       |
| [MaaldoCom.Api.Domain](src/MaaldoCom.Api.Domain/README.md)                 | [Tests.Unit.Domain](tests/Tests.Unit.Domain/README.md)                 |
|                                                                            | [Tests.Integration](tests/Tests.Integration/README.md)                 |


## MicroK8s Support

I'm working on adding support for a MicroK8s deployment running on a Raspberry Pi cluster. Will document this as I go.

### Setup

#### Azure Edge SQL Database

```yaml
services:
    sqledge:
    image: mcr.microsoft.com/azure-sql-edge:latest
    container_name: sqledge
    environment:
      ACCEPT_EULA: Y
      MSSQL_SA_PASSWORD: ${MSSQL_SA_PASSWORD}
      MSSQL_PID: Developer # Developer edition
    ports:
      - "1433:1433"
    volumes:
      - data:/var/opt/mssql
    restart: unless-stopped

volumes:
  data:
```

_.env_

```yaml
MSSQL_SA_PASSWORD="MY_SUPER_SECRET_PASSWORD"
```

