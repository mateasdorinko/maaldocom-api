<img src="assets/logo.svg" alt="logo" width="100" />

# Tests.Unit.Architecture

**Architecture tests that enforce structural rules — layer dependencies, naming conventions, visibility, and co-location — across all four source assemblies using ArchUnitNET.**

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

Uses ArchUnitNET to assert architectural rules across the compiled assemblies. Tests are grouped into four categories: layer dependency rules, naming convention enforcement, type visibility rules, and co-location rules. All four source assemblies are loaded via `ArchLoader` in `BaseTest` and shared across all test classes.

## Purpose

Prevent architectural drift by encoding structural rules as executable tests that fail the CI pipeline when violated.

## Responsibilities

- Enforce layer dependency direction (e.g., Domain must not depend on Application, Infrastructure, or Api)
- Enforce naming conventions (e.g., `Command`, `CommandHandler`, `Query`, `QueryHandler`, `Validator` suffixes)
- Enforce type visibility rules per layer
- Enforce co-location conventions (types reside in expected namespaces relative to their layer)

## What Belongs Here

- Architecture rule tests organized by category and layer: `LayerDependencyTests/`, `NamingConventionTests/`, `VisibilityTests/`, `ColocationTests/`
- `BaseTest.cs` with shared assembly layer definitions and the `Architecture` instance

## What Does Not Belong Here

- Unit tests for specific type behaviors
- Tests requiring faking or I/O
- Business logic assertions

## Dependencies

- `MaaldoCom.Api`
- `MaaldoCom.Api.Application`
- `MaaldoCom.Api.Domain`
- `MaaldoCom.Api.Infrastructure`

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `BaseTest` | Defines `DomainLayer`, `ApplicationLayer`, `InfrastructureLayer`, `ApiLayer` providers and the shared `Architecture` instance loaded from all four assemblies |
| `LayerDependencyTests/` | One class per layer; asserts forbidden outward dependencies |
| `NamingConventionTests/` | One class per layer; asserts type name suffix rules |
| `VisibilityTests/` | One class per layer; asserts access modifier rules |
| `ColocationTests/` | One class per layer; asserts namespace and folder placement rules |

## How Is It Tested

This project is the test suite. Each test method uses the ArchUnitNET fluent API:

```csharp
Types().That().Are(DomainLayer).Should().NotDependOnAny(ApplicationLayer).Check(Architecture);
```

Tests are grouped by rule category (folder) and by layer (class within that folder). No mocking or I/O is involved — ArchUnitNET reflects on the compiled assemblies directly.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Domain](../../src/MaaldoCom.Api.Domain/README.md)
- [MaaldoCom.Api.Application](../../src/MaaldoCom.Api.Application/README.md)
- [MaaldoCom.Api.Infrastructure](../../src/MaaldoCom.Api.Infrastructure/README.md)
- [MaaldoCom.Api](../../src/MaaldoCom.Api/README.md)
