# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```shell
# Build the entire solution
dotnet build MaaldoCom.Api.sln

# Run all tests
dotnet test MaaldoCom.Api.sln

# Run a single test project
dotnet test tests/Tests.Unit.Application/Tests.Unit.Application.csproj

# Run a specific test by name
dotnet test --filter "FullyQualifiedName~MyTestClass.MyTestMethod"

# Run the API locally
dotnet run --project src/MaaldoCom.Api/MaaldoCom.Api.csproj
```

### Entity Framework Migrations

Run from the `src/MaaldoCom.Api.Infrastructure` directory:

```shell
# Add migration
dotnet ef migrations add [NAME] --output-dir Database/Migrations --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj

# Apply migrations
dotnet ef database update --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj

# Remove last migration
dotnet ef migrations remove --startup-project ../MaaldoCom.Api/MaaldoCom.Api.csproj
```

## Architecture

Clean Architecture with .NET 10.0. Four source projects, five test projects. Centralized package management via `Directory.Packages.props`. `Directory.Build.props` enforces nullable, implicit usings, warnings-as-errors, and SonarAnalyzer on all projects.

### Layer Dependency Flow

**Domain** (no dependencies) → **Application** (references Domain) → **Infrastructure** (references Application) → **Api** (references Infrastructure)

- **Domain** — Entities inheriting `BaseEntity` (Guid Id) or `BaseAuditableEntity` (adds Created/Modified/Active audit fields). Core types: Knowledge, MediaAlbum, Media, Tag.
- **Application** — Custom mediator pattern. Interfaces: `IQuery<TResponse>` / `IQueryHandler<TQuery, TResponse>` and `ICommand<TResponse>` / `ICommandHandler<TCommand, TResponse>`. Commands take a `ClaimsPrincipal`. Handlers registered via Scrutor assembly scanning with decorator pipeline (validation on commands, logging on all). Results wrapped in `FluentResults.Result<T>` with custom error types (`EntityNotFoundError`, `DuplicateEntityCreationError`, `ValidationFailureError`, `BlobNotFoundError`). DTOs with FluentValidation validators.
- **Infrastructure** — EF Core with SQL Server (`MaaldoComDbContext`), audit tracking on save, FusionCache hybrid caching (`ICacheManager`), Azure Blob Storage (`IBlobProvider`), Mailgun email (`IEmailProvider`). All services registered via `AddInfrastructureServices()` extension.
- **Api** — FastEndpoints. Each endpoint class overrides `Configure()` (route, auth, caching) and `HandleAsync()`. Queries/commands executed via `ExecuteAsync()`. Results matched with `result.Match(onSuccess, onFailure)` to send appropriate HTTP responses. Response models extend `BaseModel` with HATEOAS `href`. API docs served via Scalar at `/docs`.

### Key Patterns

- **Endpoint pattern**: `Endpoint<TRequest, TResponse>` or `EndpointWithoutRequest<TResponse>`. Configure route/auth in `Configure()`, handle logic in `HandleAsync()`.
- **Query/Command dispatch**: Handlers are constructor-injected into endpoints (e.g., `IQueryHandler<ListKnowledgeQuery, IEnumerable<KnowledgeDto>> handler`). Instantiate the query/command, call `handler.HandleAsync(query, ct)`. Scrutor scans the Application assembly to register all `IQueryHandler<,>` and `ICommandHandler<,>` implementations as scoped services. Decorator pipeline applies validation (commands only) then logging.
- **Caching**: `ICacheManager` wraps FusionCache (L1 in-memory + L2 distributed). 20-minute default TTL. Cache methods per entity type (e.g., `ListKnowledgeAsync`).
- **Error handling**: Return `Result.Fail()` with typed errors, match in endpoints to map to HTTP status codes. Global `DefaultExceptionHandler` middleware catches unhandled exceptions.
- **Auth**: JWT Bearer via Auth0. Endpoints default to requiring auth; use `AllowAnonymous()` to opt out.

### Testing

xUnit + Shouldly assertions + FakeItEasy mocking. Test projects mirror source project structure (`Tests.Unit.Api`, `Tests.Unit.Application`, etc.). Coverage collected via coverlet.

**Naming conventions** — Test class name = method under test (e.g. `HandleAsync`, `Configure`, `ToPostModel`). Test method name follows `MethodName_Context_ExpectedResult`. Assertions use `ShouldBe` / `ShouldBeEquivalentTo` (Shouldly); null-guard tests use `Assert.Throws<ArgumentNullException>`.

**Endpoint unit tests** — Use `Factory.Create<TEndpoint>(handler)` to instantiate an endpoint with faked constructor dependencies. Two test classes per endpoint: `Configure.cs` (calls `endpoint.Configure()`, asserts verb/route/auth on `endpoint.Definition`) and `HandleAsync.cs` (calls `endpoint.HandleAsync(request, ct)`, asserts status code and `endpoint.Response`).

**`endpoint.Response`** — Only populated when using typed send methods (`Send.OkAsync<TResponse>`, `Send.CreatedAtAsync<TEndpoint>(routeValues, responseBody: TResponse, ...)`). Endpoints must extend `EndpointWithoutRequest<TResponse>` (not the non-generic `EndpointWithoutRequest`) for `endpoint.Response` to be typed.

**`endpoint.ValidationFailures`** — Populated by `AddError(message)` in `HandleAsync`. Assert on this collection to verify broken-rules behaviour in failure paths.

**Endpoints using `Send.CreatedAtAsync`** — Requires `LinkGenerator` in the endpoint's service provider. Inject a fake via `Factory.Create<TEndpoint>(ctx => ctx.AddTestServices(s => s.AddSingleton<LinkGenerator>(lg)), handler)`. Fake the abstract `GetPathByAddress<string>(HttpContext, string, RouteValueDictionary, ...)` method — `GetPathByName` is an extension method and cannot be intercepted by FakeItEasy.

**Sealed classes / extension methods** — FakeItEasy cannot fake sealed classes (`WebApplication`, `FileInfo` effectively) or intercept extension methods. For sealed-class dependencies, either test against real instances (e.g. real temp files via `Directory.CreateTempSubdirectory()`) or extract an internal overload that accepts the equivalent interface. Test classes implementing `IDisposable` must be `sealed` (SonarAnalyzer S3881). Use xUnit's `IDisposable` pattern for per-test setup/teardown — xUnit creates a new class instance per test and calls `Dispose` after each.
