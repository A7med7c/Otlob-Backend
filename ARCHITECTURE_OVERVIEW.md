# Architecture Overview

## 1) High-Level Architecture
The solution follows a multi-project layered architecture:
- **Presentation layer** (`Infrastructure/PresentationLayer`, `E-Commerce.Web`)
- **Application/service layer** (`Core/ServiceLayer` + contracts in `Core/SeviceImplementation`)
- **Domain layer** (`Core/DomainLayer`)
- **Infrastructure layer** (`Infrastructure/Persistence`)
- **Shared contracts** (`Shared`)

This mostly aligns with Clean Architecture goals, but there are coupling and naming consistency gaps.

## 2) Runtime Components
- **API Host**: `E-Commerce.Web` (`Program.cs`)
- **Controllers**: product, basket, orders, user
- **Business services**: product, basket, order, authentication, cache
- **Persistence**:
  - SQL Server via EF Core (`ApplicationDbContext`, `ApplicationIdentityDbContext`)
  - Redis via StackExchange.Redis (basket + response cache repositories)
- **Cross-cutting**:
  - JWT authentication
  - custom exception middleware
  - model validation response factory
  - AutoMapper

## 3) Dependency Direction
Expected:
- Presentation → Service abstraction
- Service implementation → Domain contracts/models + shared DTOs
- Persistence → Domain contracts/models

Observed issues:
1. `Core/SeviceImplementation` naming typo leaks across solution and weakens maintainability.
2. `ServicesManager` manually constructs concrete services (`new`) which bypasses DI composition benefits.
3. Caching abstraction exists but implementation is not registered in DI (runtime break on cached endpoint).

## 4) Data Architecture
- **Primary DB**: SQL Server (`DefaultConnection`) for products/orders
- **Identity DB**: SQL Server (`IdentityConnection`) for users/roles/addresses
- **Cache/Session store**: Redis (`RedisConnection`) for baskets and action-level response caching
- **Migrations**:
  - Product/order schema under `Infrastructure/Persistence/Data/Migrations`
  - Identity schema under `Infrastructure/Persistence/Identity/Migrations`

## 5) Integration Points
- SQL Server
- Redis
- Swagger/OpenAPI
- JWT token issuer/audience validation

No external payment gateway, monitoring system, or message broker integration is present.

## 6) Architectural Strengths
- Clear layer split across projects
- Good use of repository/specification patterns
- Separation of DTOs from domain entities
- Centralized exception middleware

## 7) Architectural Risks
| Severity | Location | Issue | Recommended Fix |
|---|---|---|---|
| High | `Infrastructure/PresentationLayer/Attributes/CashAttribute.cs:17` + `Infrastructure/Persistence/InfrastuctureServicesCollection.cs` | `ICashService` used but not registered -> runtime failure on `[Cash]` endpoint | Register `ICashService` and `ICashRepository` in DI |
| High | `E-Commerce.Web/appsettings.json` | Hardcoded JWT key + machine-specific SQL connections in source | Move to environment variables/user secrets/secret manager |
| Medium | `Core/ServiceLayer/ServicesManager.cs` | Service locator-like lazy manual instantiation (`new`) increases coupling and test friction | Register and inject concrete services directly through DI |
| Medium | `Core/ServiceLayer/Specifications/*.cs` | Duplicate filtering logic across specs | Introduce reusable filter builder/spec base helper |
| Medium | `Shared/ProductQueryParams.cs` | `PageSize` / `PageIndex` defaults can produce invalid paging behavior | Set safe defaults and enforce min/max guards |

## 8) Recommended Target Architecture
- Keep layered structure, but improve boundaries:
  1. Replace `ServicesManager` with direct DI for each service
  2. Add validation pipeline (FluentValidation/DataAnnotations hardening)
  3. Externalize configuration and secrets
  4. Add observability (structured logging + health checks + metrics)
  5. Add CI/CD + container runtime descriptors
