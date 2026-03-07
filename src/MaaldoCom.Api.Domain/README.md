<img src="assets/logo.svg" alt="logo" width="100" />

# MaaldoCom.Api.Domain

**The innermost layer of the solution; defines all domain entities and core types with no external dependencies.**

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

Domain contains the core entity model for the solution. All entities inherit from `BaseEntity` or `BaseAuditableEntity`. There are no framework dependencies — the project is a plain class library with no references to ASP.NET Core, EF Core, or any application-layer concern.

## Purpose

Define the core domain model: entities, base types, and domain-level helpers that represent the business concepts of the application.

## Responsibilities

- Define all entity classes (`Knowledge`, `MediaAlbum`, `Media`, `Tag`, `MediaAlbumTag`, `MediaTag`)
- Define `BaseEntity` (Guid Id) and `BaseAuditableEntity` (adds `Created`, `LastModified`, `CreatedBy`, `LastModifiedBy`, `Active`)
- Provide domain-level helpers and constants (e.g., `MediaAlbumHelper`, media album constants)
- Provide domain-scoped extensions that operate on standard or domain types (e.g., `SecurityExtensions` for `ClaimsPrincipal`)

## What Belongs Here

- Entity classes that represent business concepts
- Base entity types with shared identity and audit fields
- Domain helpers and constants with no infrastructure dependency
- Extensions that operate purely on domain or standard framework types

## What Does Not Belong Here

- EF Core configurations, attributes, or data annotations
- Application-layer interfaces (`IQueryHandler`, `ICacheManager`, etc.)
- DTOs or view models
- Any reference to ASP.NET Core, EF Core, or external libraries

## Dependencies

None. This project has no project references.

## Key Entry Points / Important Types

| Type | Purpose |
|---|---|
| `BaseEntity` | Root base class; provides `Guid Id` |
| `BaseAuditableEntity` | Extends `BaseEntity`; adds `Created`, `LastModified`, `CreatedBy`, `LastModifiedBy`, `Active` |
| `Knowledge` | Represents a knowledge entry |
| `MediaAlbum` | Represents a media album |
| `Media` | Represents an individual media item within an album |
| `Tag` | Represents a categorization tag |
| `MediaAlbumTag` / `MediaTag` | Join entities for many-to-many tag relationships |
| `MediaAlbumHelper` | Domain helpers for media album operations |

## How Is It Tested

Tested by [`Tests.Unit.Domain`](../../tests/Tests.Unit.Domain/README.md). Tests target entity behavior and domain helper logic directly — no mocking is typically required given the absence of external dependencies.

Test class names reflect the type under test (e.g., `MediaAlbumHelper`). Test method names follow `MethodName_Context_ExpectedResult`. Namespaces in the test project mirror the SUT: `MaaldoCom.Api.Domain.Entities` → `Tests.Unit.Domain.Entities`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Application](../MaaldoCom.Api.Application/README.md) — consumes domain entities
- [Tests.Unit.Domain](../../tests/Tests.Unit.Domain/README.md)
