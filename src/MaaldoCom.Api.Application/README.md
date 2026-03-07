<img src="/assets/logo.svg" alt="logo" width="100" />

# MaaldoCom.Api.Application

**Defines all use cases for the solution via a custom mediator pattern, with no dependency on infrastructure or HTTP concerns.**

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

## Overview

Application is the use-case layer. Queries and commands are plain types implementing `IQuery<TResponse>` or `ICommand<TResponse>`, with corresponding handler interfaces. Handlers are registered via Scrutor and decorated with a validation pipeline (commands only) and a logging pipeline (all handlers). Results are wrapped in `FluentResults.Result<T>`. Infrastructure concerns are accessed only through interfaces defined here.

## Purpose

Orchestrate application use cases — querying and mutating data — while keeping all infrastructure and HTTP concerns behind interfaces. This layer owns the contract; Infrastructure and Api satisfy it.

## Responsibilities

- Define query and command types and their handler interfaces
- Implement query and command handlers
- Define interfaces for external dependencies (`IMaaldoComDbContext`, `ICacheManager`, `IBlobsProvider`, `IEmailProvider`)
- Validate commands via FluentValidation in the decorator pipeline
- Log all handler executions via the logging decorator
- Map domain entities to DTOs via `MapperExtensions`
- Define typed error types (`EntityNotFoundError`, `DuplicateEntityCreationError`, `ValidationFailureError`, `BlobNotFoundError`)

## What Belongs Here

- `IQuery<T>`, `ICommand<T>`, `IQueryHandler<,>`, `ICommandHandler<,>` and their implementations
- DTOs and FluentValidation validators for those DTOs
- Application-level interfaces for infrastructure services
- Decorator behaviors (`ValidationDecorator`, `LoggingDecorator`)
- Mapping extensions from domain entities to DTOs
- Custom `FluentResults` error types

## What Does Not Belong Here

- Concrete implementations of `ICacheManager`, `IBlobsProvider`, `IEmailProvider`, or `IMaaldoComDbContext`
- EF Core or any database provider dependency
- FastEndpoints, HTTP request/response types, or routing
- Any reference to the Infrastructure or Api projects

## Dependencies

- `MaaldoCom.Api.Domain`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `IQuery<TResponse>` / `IQueryHandler<TQuery, TResponse>` | Query contract and handler interface |
| `ICommand<TResponse>` / `ICommandHandler<TCommand, TResponse>` | Command contract and handler interface |
| `ValidationDecorator` | Runs FluentValidation before command handler execution |
| `LoggingDecorator` | Logs entry and exit for all query and command handlers |
| `ICacheManager` | Cache abstraction; implemented in Infrastructure |
| `IMaaldoComDbContext` | Database abstraction; implemented in Infrastructure |
| `IBlobsProvider` | Blob storage abstraction |
| `IEmailProvider` | Email abstraction |
| `AddApplicationServices()` | Registers handlers via Scrutor and wires the decorator pipeline |

## How Is It Tested

Tested by [`Tests.Unit.Application`](../../tests/Tests.Unit.Application/README.md). Handlers are tested in isolation with all dependencies faked via FakeItEasy. `DbSet<T>` fakes use `DbSetHelper.CreateFakeDbSet<T>` for async LINQ support.

Test class names reflect the method under test (e.g., `HandleAsync`). Test method names follow `MethodName_Context_ExpectedResult`. Namespaces mirror the SUT: `MaaldoCom.Api.Application.Queries.Knowledge` → `Tests.Unit.Application.Queries.Knowledge`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Domain](../MaaldoCom.Api.Domain/README.md) — domain entities consumed here
- [MaaldoCom.Api.Infrastructure](../MaaldoCom.Api.Infrastructure/README.md) — implements interfaces defined here
- [Tests.Unit.Application](../../tests/Tests.Unit.Application/README.md)
