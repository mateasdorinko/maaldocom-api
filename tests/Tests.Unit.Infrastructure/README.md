<img src="/assets/logo.svg" alt="logo" width="100" />

# Tests.Unit.Infrastructure

**Unit tests for the `MaaldoCom.Api.Infrastructure` layer, covering caching, blob storage, email, and database context behavior.**

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

Contains unit tests for all infrastructure implementations: `CacheManager`, `AzureStorageBlobsProvider`, `MailGunEmailProvider`, and `MaaldoComDbContext`. External SDK dependencies are faked where possible; sealed SDK types that cannot be intercepted by FakeItEasy are tested against real temp resources (e.g., temp directories, local Azurite).

## Purpose

Verify that infrastructure implementations correctly satisfy their Application-layer interface contracts in isolation from live external services.

## Responsibilities

- Test `CacheManager` cache operations
- Test `AzureStorageBlobsProvider` blob read/write operations
- Test `MailGunEmailProvider` email dispatch
- Test `MaaldoComDbContext` audit tracking behavior on save

## What Belongs Here

- Tests targeting types in `MaaldoCom.Api.Infrastructure`, organized by feature folder

## What Does Not Belong Here

- Tests for application handlers or endpoint logic
- Integration tests against real databases or live Azure services

## Dependencies

- `MaaldoCom.Api.Infrastructure`

## Key Entry Points / Important Types

Tests are organized by feature folder mirroring the Infrastructure project structure: `Cache/`, `Blobs/`, `Email/`, `Database/`.

## How Is It Tested

This project is the test suite. Test class name = method under test. Method names follow `MethodName_Context_ExpectedResult`.

Namespace pattern mirrors the SUT: `MaaldoCom.Api.Infrastructure.Cache` → `Tests.Unit.Infrastructure.Cache`.

Sealed SDK classes (e.g., `BlobServiceClient`) cannot be faked by FakeItEasy — use real instances backed by temp directories or local Azurite where applicable. Test classes implementing `IDisposable` must be `sealed` (SonarAnalyzer S3881). Use xUnit's `IDisposable` pattern for per-test setup and teardown.

## Related Documentation

- [Solution README](../../README.md)
- [MaaldoCom.Api.Infrastructure](../../src/MaaldoCom.Api.Infrastructure/README.md) — system under test
