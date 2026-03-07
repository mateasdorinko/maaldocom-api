<img src="assets/logo.svg" alt="logo" width="100" />

# Tests.Integration

**Integration tests that exercise the full HTTP pipeline by booting the application in-process via FastEndpoints.Testing's `AppFixture`.**

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

Uses FastEndpoints.Testing to boot a real instance of the application in-process. Tests send actual HTTP requests and assert on response status codes and payloads, exercising the full stack from endpoint through handler to infrastructure. This project is in its early stages; coverage will grow as integration scenarios are defined.

## Purpose

Verify end-to-end HTTP behavior — routing, authentication, middleware, and handler integration — against a running application instance backed by real or test-double infrastructure.

## Responsibilities

- Boot the application using `AppFixture` from FastEndpoints.Testing
- Send HTTP requests and assert on response status codes and payloads
- Cover scenarios that require the full stack working together (endpoint → handler → database)

## What Belongs Here

- Integration tests organized by feature, mirroring the endpoint folder structure
- `AppFixture` configuration and any shared application factory setup

## What Does Not Belong Here

- Unit tests for isolated types (handlers, endpoints, domain logic)
- Architecture rule assertions
- Tests that fake Application or Infrastructure layer services

## Dependencies

- `MaaldoCom.Api`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `AppFixture` (FastEndpoints.Testing) | Boots the full application in-process and provides an HTTP client for test requests |

## How Is It Tested

This project is the test suite. Tests are organized by feature folder matching the endpoint structure. Integration tests require a running database and supporting local services (SQL Server, Azurite) — see the [Getting Started](../../README.md#getting-started) section of the solution README for setup.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api](../../src/MaaldoCom.Api/README.md)
- [MaaldoCom.Api.Infrastructure](../../src/MaaldoCom.Api.Infrastructure/README.md) — services exercised end-to-end
