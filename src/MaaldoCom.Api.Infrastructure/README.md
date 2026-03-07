<img src="/assets/logo.svg" alt="logo" width="100" />

# MaaldoCom.Api.Infrastructure

**Implements all external dependencies — database, caching, blob storage, and email — as defined by the Application layer's interfaces.**

## Table of Contents

- [Overview](#overview)
- [Purpose](#purpose)
- [Responsibilities](#responsibilities)
- [What Belongs Here](#what-belongs-here)
- [What Does Not Belong Here](#what-does-not-belong-here)
- [Dependencies](#dependencies)
- [Key Entry Points / Important Types](#key-entry-points--important-types)
- [How Is It Tested](#how-is-it-tested)
- [Related Documentation](#related-documentation)
- [Local Development](#local-development)

## Overview

Infrastructure contains all concrete implementations of the interfaces defined in Application. It owns the EF Core `DbContext`, entity configurations, database migrations, the FusionCache-based cache manager, Azure Blob Storage integration, and Mailgun email. All services are registered through a single `AddInfrastructureServices()` extension method.

## Purpose

Satisfy the infrastructure contracts defined by the Application layer, connecting the solution to SQL Server, Azure Blob Storage, Mailgun, and the hybrid cache.

## Responsibilities

- Implement `IMaaldoComDbContext` via `MaaldoComDbContext` (EF Core, SQL Server, audit tracking on save)
- Configure entity mappings via `IEntityTypeConfiguration<T>` per entity
- Manage database migrations and seeding (`Database/Migrations/`)
- Implement `ICacheManager` via FusionCache (L1 in-memory + L2 distributed, 20-minute default TTL)
- Implement `IBlobsProvider` via `AzureStorageBlobsProvider`
- Implement `IEmailProvider` via `MailGunEmailProvider`
- Register all infrastructure services via `AddInfrastructureServices()`

## What Belongs Here

- `MaaldoComDbContext` and EF Core entity configurations
- Database migrations and seeders
- `CacheManager` and cache-related infrastructure
- `AzureStorageBlobsProvider` and `MailGunEmailProvider`
- `AddInfrastructureServices()` service registration extension

## What Does Not Belong Here

- Business logic or use-case orchestration
- Application-layer interfaces (defined in Application)
- FastEndpoints or HTTP concerns
- Any dependency on `MaaldoCom.Api` (the presentation layer)

## Dependencies

- `MaaldoCom.Api.Application`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `MaaldoComDbContext` | EF Core DbContext; implements `IMaaldoComDbContext`; tracks auditable entities on save |
| `CacheManager` | Implements `ICacheManager`; wraps FusionCache hybrid cache |
| `AzureStorageBlobsProvider` | Implements `IBlobsProvider`; uses Azure Blob Storage SDK |
| `MailGunEmailProvider` | Implements `IEmailProvider`; uses Mailgun REST API |
| `AddInfrastructureServices()` | Single registration entry point for all infrastructure services |

## How Is It Tested

Tested by [`Tests.Unit.Infrastructure`](../../tests/Tests.Unit.Infrastructure/README.md). External SDK dependencies are faked where possible; sealed SDK types are tested against real temp resources (e.g., temp directories, local Azurite).

Test class names reflect the method under test. Test method names follow `MethodName_Context_ExpectedResult`. Namespaces mirror the SUT: `MaaldoCom.Api.Infrastructure.Cache` → `Tests.Unit.Infrastructure.Cache`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Application](../MaaldoCom.Api.Application/README.md) — interfaces implemented here
- [Tests.Unit.Infrastructure](../../tests/Tests.Unit.Infrastructure/README.md)

---

## Local Development

### Entity Framework

Run all migration commands from the `MaaldoCom.Api.Infrastructure` project directory.

**Install EF CLI** (once per machine):

```shell
dotnet tool install --global dotnet-ef
```

**Add a migration:**

```shell
dotnet ef migrations add [MIGRATION_NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

**Apply migrations:**

```shell
dotnet ef database update --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

**Remove last migration:**

```shell
dotnet ef migrations remove --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

### FFMpeg

FFMpeg is required for media processing and must be on the system PATH.

**Debian/Ubuntu:**

```shell
sudo apt update && sudo apt install ffmpeg
```

**Windows:** Download from [ffmpeg.org](https://ffmpeg.org/download.html), extract, and add the `bin/` folder to your system PATH. Verify with `ffmpeg -version`.
