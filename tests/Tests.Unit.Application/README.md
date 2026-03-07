<img src="/assets/logo.svg" alt="logo" width="100" />

# Tests.Unit.Application

**Unit tests for the `MaaldoCom.Api.Application` layer, covering query handlers, command handlers, decorators, validators, and DTO mappings.**

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

Contains unit tests for all application-layer handlers, decorator behaviors, FluentValidation validators, and mapping extensions. All external dependencies (`IMaaldoComDbContext`, `ICacheManager`, `IBlobsProvider`, `IEmailProvider`) are faked via FakeItEasy. `DbSet<T>` fakes use `DbSetHelper.CreateFakeDbSet<T>` for async LINQ support.

## Purpose

Verify handler logic, decorator behavior (validation, logging), and DTO mapping correctness in isolation from infrastructure.

## Responsibilities

- Test query handler and command handler `HandleAsync()` for all success and failure cases
- Test `ValidationDecorator` and `LoggingDecorator` behavior
- Test FluentValidation validators (valid and invalid inputs)
- Test `MapperExtensions` mapping from domain entities to DTOs

## What Belongs Here

- Handler test classes (one per handler method, organized to mirror the SUT namespace)
- Decorator behavior tests
- Validator tests
- Mapping extension tests
- `DbSetHelper` and other shared test infrastructure for this project

## What Does Not Belong Here

- Tests for endpoint routing or HTTP response codes
- Tests for EF Core configurations or migrations
- Integration-style tests requiring real databases or services

## Dependencies

- `MaaldoCom.Api.Application`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `DbSetHelper.CreateFakeDbSet<T>(data)` | Creates a fake `DbSet<T>` backed by an async-capable query provider for LINQ operator support |
| FakeItEasy fakes of `IMaaldoComDbContext`, `ICacheManager`, etc. | Replace infrastructure dependencies in handler tests |

## How Is It Tested

This project is the test suite. Test class name = method under test (e.g., `HandleAsync`). Method name pattern: `HandleAsync_WhenEntityNotFound_ReturnsEntityNotFoundError`.

Namespace pattern mirrors the SUT: `MaaldoCom.Api.Application.Queries.Knowledge` → `Tests.Unit.Application.Queries.Knowledge`.

Use `TestContext.Current.CancellationToken` for all async calls. Use `A<CancellationToken>._` for FakeItEasy setups where the token is not forwarded. Declare shared test types `internal` (not `file`-scoped) to keep them accessible to `DynamicProxyGenAssembly2`. Ensure `[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]` is present in `AssemblyInfo.cs`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Application](../../src/MaaldoCom.Api.Application/README.md) — system under test
- [Tests.Unit.Api](../Tests.Unit.Api/README.md)
