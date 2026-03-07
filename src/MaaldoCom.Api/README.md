<img src="assets/logo.svg" alt="logo" width="100" />

# MaaldoCom.Api

**The presentation layer; exposes all HTTP endpoints via FastEndpoints and serves API documentation via Scalar.**

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

MaaldoCom.Api is the outermost layer of the solution. It defines all HTTP endpoints using FastEndpoints, maps Application DTOs to HTTP response models, configures authentication, CORS, caching middleware, and OpenTelemetry, and exposes the Scalar API documentation UI at `/docs`. All business logic is delegated to Application-layer handlers via constructor-injected handler interfaces.

## Purpose

Accept HTTP requests, delegate to the appropriate query or command handler, map results to HTTP responses, and return the appropriate status code.

## Responsibilities

- Define FastEndpoints endpoint classes organized by feature folder under `Endpoints/`
- Configure route, HTTP verb, auth policy, and response caching per endpoint in `Configure()`
- Delegate to `IQueryHandler` or `ICommandHandler` in `HandleAsync()`
- Map Application DTOs to response models via `MapperExtensions`
- Configure the middleware pipeline and service registrations in `Program.cs`
- Serve API documentation at `/docs` via Scalar

## What Belongs Here

- FastEndpoints endpoint classes and their request/response models
- Mapping extensions from DTOs to API response models (`MapperExtensions.*`)
- `BaseModel` (HATEOAS `href` support) and `UrlMaker`
- `Program.cs` and all service/middleware registration extensions
- Static assets

## What Does Not Belong Here

- Business logic or data access
- Direct use of `DbContext` or infrastructure services
- Application-layer interfaces (defined in Application)
- Domain entity types exposed directly in response models — map through DTOs first

## Dependencies

- `MaaldoCom.Api.Infrastructure` (transitively includes Application and Domain)

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `Program.cs` | Application entry point; configures services and middleware pipeline |
| `BaseModel` | Base response model; provides `href` for HATEOAS links |
| `UrlMaker` | Centralizes route name constants for `LinkGenerator` usage |
| Endpoint classes (e.g., `ListKnowledgeEndpoint`, `PostMediaAlbumEndpoint`) | One class per HTTP operation, organized under `Endpoints/<Feature>/` |
| `MapperExtensions` | Extension methods mapping DTOs to HTTP response models |

## How Is It Tested

Tested by [`Tests.Unit.Api`](../../tests/Tests.Unit.Api/README.md). Each endpoint has two test classes:

- `Configure.cs` — instantiates the endpoint via `Factory.Create<TEndpoint>`, calls `Configure()`, and asserts verb, route, and auth on `endpoint.Definition`.
- `HandleAsync.cs` — calls `HandleAsync(request, ct)` with a faked handler and asserts `endpoint.Response` and HTTP status code.

Test class names reflect the method under test (`Configure`, `HandleAsync`). Test method names follow `MethodName_Context_ExpectedResult`. Namespaces mirror the SUT: `MaaldoCom.Api.Endpoints.Knowledge` → `Tests.Unit.Api.Endpoints.Knowledge`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Application](../MaaldoCom.Api.Application/README.md) — handler interfaces injected here
- [Tests.Unit.Api](../../tests/Tests.Unit.Api/README.md)
