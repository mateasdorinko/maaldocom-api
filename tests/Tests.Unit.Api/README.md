<img src="assets/logo.svg" alt="logo" width="100" />

# Tests.Unit.Api

**Unit tests for the `MaaldoCom.Api` presentation layer, covering endpoint configuration and request handling in isolation.**

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

Contains unit tests for all FastEndpoints endpoint classes in `MaaldoCom.Api`. Each endpoint is covered by two focused test classes — one for `Configure()` and one for `HandleAsync()` — using `Factory.Create<TEndpoint>` to construct endpoints with faked handler dependencies.

## Purpose

Verify that each endpoint is correctly configured (route, verb, auth policy) and correctly handles success and failure paths without hitting real infrastructure.

## Responsibilities

- Test `Configure()` on every endpoint: verb, route, auth, and response caching settings
- Test `HandleAsync()` success and failure paths with faked handlers
- Assert `endpoint.Response`, `endpoint.ValidationFailures`, and HTTP status codes
- Cover `MapperExtensions` mapping from DTOs to response models

## What Belongs Here

- `Configure.cs` and `HandleAsync.cs` test classes per endpoint, nested under a folder matching the endpoint name
- Tests for `MapperExtensions` (DTO → response model mappings)
- FakeItEasy setups for `IQueryHandler` and `ICommandHandler` dependencies

## What Does Not Belong Here

- Tests for Application handlers or domain logic
- Integration-style tests that spin up the full HTTP pipeline
- Tests for infrastructure implementations

## Dependencies

- `MaaldoCom.Api`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `Factory.Create<TEndpoint>(handler)` | Constructs an endpoint with faked constructor dependencies for unit testing |
| `endpoint.Definition` | Inspected in `Configure` tests for verb, route, and auth settings |
| `endpoint.Response` | Asserted in `HandleAsync` tests for typed response content |
| `endpoint.ValidationFailures` | Asserted when `AddError()` failure paths are exercised |

## How Is It Tested

This project is the test suite. Each endpoint in `MaaldoCom.Api` has a corresponding test folder with:

- `Configure.cs` — class name `Configure`, methods such as `Configure_Always_SetsVerb`, `Configure_Always_SetsRoute`, `Configure_Always_RequiresAuth`
- `HandleAsync.cs` — class name `HandleAsync`, methods such as `HandleAsync_WhenHandlerSucceeds_Returns200Ok`, `HandleAsync_WhenEntityNotFound_Returns404NotFound`

Namespace pattern mirrors the SUT: `MaaldoCom.Api.Endpoints.Knowledge.GetKnowledgeByIdEndpoint` → `Tests.Unit.Api.Endpoints.Knowledge.GetKnowledgeByIdEndpoint`.

Use `TestContext.Current.CancellationToken` for all async test calls. Use `A<CancellationToken>._` in FakeItEasy setups for methods that do not forward the test token. Endpoints using `Send.CreatedAtAsync` require a faked `LinkGenerator` injected via `Factory.Create<TEndpoint>(ctx => ctx.AddTestServices(...), handler)`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api](../../src/MaaldoCom.Api/README.md) — system under test
- [Tests.Unit.Application](../Tests.Unit.Application/README.md)
