<img src="assets/logo.svg" alt="logo" width="100" />

# Tests.Unit.Domain

**Unit tests for the `MaaldoCom.Api.Domain` layer, covering entity behavior and domain-level helpers.**

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

Contains unit tests for domain entities and helpers. Because the Domain layer has no external dependencies, these tests are straightforward: no mocking is required and no I/O is involved. Tests exercise entity construction, property state, and domain helper logic directly.

## Purpose

Verify domain entity behavior, base type contracts, and domain helper logic.

## Responsibilities

- Test entity constructors, property assignments, and derived state
- Test domain helpers (e.g., `MediaAlbumHelper`)
- Test domain extensions (e.g., `SecurityExtensions`)

## What Belongs Here

- Tests targeting types in `MaaldoCom.Api.Domain`

## What Does Not Belong Here

- Tests for application handlers, infrastructure services, or endpoints
- Any test requiring faking or I/O

## Dependencies

- `MaaldoCom.Api.Domain`

## Key Entry Points / Important Types

Tests map directly to types in the Domain project. No shared test infrastructure is needed beyond xUnit and Shouldly.

## How Is It Tested

This project is the test suite. Test class name = type under test (e.g., `MediaAlbumHelper`). Method names follow `MethodName_Context_ExpectedResult`.

Namespace pattern mirrors the SUT: `MaaldoCom.Api.Domain.Entities` → `Tests.Unit.Domain.Entities`.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Domain](../../src/MaaldoCom.Api.Domain/README.md) — system under test
