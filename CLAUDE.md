# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```shell
# Build the entire solution
dotnet build MaaldoCom.Services.sln

# Run all tests
dotnet test MaaldoCom.Services.sln

# Run a single test project
dotnet test tests/Tests.Unit.Application/Tests.Unit.Application.csproj

# Run a specific test by name
dotnet test --filter "FullyQualifiedName~MyTestClass.MyTestMethod"

# Run the API locally
dotnet run --project src/MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```

### Entity Framework Migrations

Run from the `src/MaaldoCom.Services.Infrastructure` directory:

```shell
# Add migration
dotnet ef migrations add [NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj

# Apply migrations
dotnet ef database update --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj

# Remove last migration
dotnet ef migrations remove --startup-project ../MaaldoCom.Services.Api/MaaldoCom.Services.Api.csproj
```

## Architecture

Clean Architecture with .NET 10.0. Five source projects, six test projects. Centralized package management via `Directory.Packages.props`. `Directory.Build.props` enforces nullable, implicit usings, warnings-as-errors, and SonarAnalyzer on all projects.

### Layer Dependency Flow

**Domain** (no dependencies) → **Application** (references Domain) → **Infrastructure** (references Application) → **Api** (references Infrastructure)

- **Domain** — Entities inheriting `BaseEntity` (Guid Id) or `BaseAuditableEntity` (adds Created/Modified/Active audit fields). Core types: Knowledge, MediaAlbum, Media, Tag.
- **Application** — Query/Command pattern using FastEndpoints messaging (`ICommand<TResult>` / `ICommandHandler<TCommand, TResult>`). Queries and commands take a `ClaimsPrincipal` via `BaseQuery`/`BaseCommand`. Handlers receive `ICacheManager` and `ILogger` via `BaseQueryHandler`/`BaseCommandHandler`. Results wrapped in `FluentResults.Result<T>` with custom error types (`EntityNotFoundError`, `DuplicateEntityCreationError`, `ValidationFailureError`, `BlobNotFoundError`). DTOs with FluentValidation validators.
- **Infrastructure** — EF Core with SQL Server (`MaaldoComDbContext`), audit tracking on save, FusionCache hybrid caching (`ICacheManager`), Azure Blob Storage (`IBlobProvider`), Mailgun email (`IEmailProvider`). All services registered via `AddInfrastructureServices()` extension.
- **Api** — FastEndpoints. Each endpoint class overrides `Configure()` (route, auth, caching) and `HandleAsync()`. Queries/commands executed via `ExecuteAsync()`. Results matched with `result.Match(onSuccess, onFailure)` to send appropriate HTTP responses. Response models extend `BaseModel` with HATEOAS `href`. API docs served via Scalar at `/docs`.
- **Cli** — Spectre.Console CLI tool. Uses Refitter source generator to auto-generate `MaaldoApiClient` from the API's OpenAPI spec (`Infrastructure/Generated/MaaldoApiClient.cs` — do not edit manually).

### Key Patterns

- **Endpoint pattern**: `Endpoint<TRequest, TResponse>` or `EndpointWithoutRequest<TResponse>`. Configure route/auth in `Configure()`, handle logic in `HandleAsync()`.
- **Query/Command dispatch**: Instantiate query/command with `User` and params, call `.ExecuteAsync(ct)`. FastEndpoints auto-discovers handlers via assembly scanning of the Application assembly.
- **Caching**: `ICacheManager` wraps FusionCache (L1 in-memory + L2 distributed). 20-minute default TTL. Cache methods per entity type (e.g., `ListKnowledgeAsync`).
- **Error handling**: Return `Result.Fail()` with typed errors, match in endpoints to map to HTTP status codes. Global `DefaultExceptionHandler` middleware catches unhandled exceptions.
- **Auth**: JWT Bearer via Auth0. Endpoints default to requiring auth; use `AllowAnonymous()` to opt out.

### Testing

xUnit + Shouldly assertions + FakeItEasy mocking. Test projects mirror source project structure (`Tests.Unit.Api`, `Tests.Unit.Application`, etc.). Coverage collected via coverlet.
