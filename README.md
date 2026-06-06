# Otlob Backend API

## Overview
Otlob Backend is a layered ASP.NET Core Web API for e-commerce operations (catalog, basket, orders, and identity). It uses SQL Server for transactional data, ASP.NET Core Identity for user management, and Redis for basket/caching scenarios.

## Features
- Product catalog listing with filtering, sorting, and pagination
- Product brands/types lookup
- Basket create/read/update/delete using Redis
- Order creation and retrieval
- Delivery methods retrieval
- User registration, login, current user profile, and address management
- JWT-based authentication and role claims
- Global exception handling middleware
- Initial data and identity seeding
- Swagger/OpenAPI in development

## Architecture
- **Style**: Layered architecture with partial DDD/Clean Architecture concepts
- **Projects**:
  - `Core/DomainLayer`: entities, contracts, exceptions
  - `Core/SeviceImplementation`: service interfaces (application contracts)
  - `Core/ServiceLayer`: service implementations, specifications, AutoMapper profiles
  - `Infrastructure/Persistence`: EF Core contexts, repositories, unit of work, Redis repositories, seeders
  - `Infrastructure/PresentationLayer`: API controllers and caching action filter
  - `E-Commerce.Web`: composition root, middleware registration, app host
  - `Shared`: DTOs, API response contracts, query/pagination models

## Tech Stack
- .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core 10 (SQL Server provider)
- ASP.NET Core Identity
- JWT bearer authentication
- AutoMapper
- StackExchange.Redis
- Swagger / Swashbuckle

## Prerequisites
- .NET SDK 10.x
- SQL Server instance
- Redis instance

## Installation
1. Clone repository.
2. Navigate to the repository root:
   ```bash
   cd /tmp/workspace/A7med7c/Otlob-Backend
   ```
3. Restore dependencies:
   ```bash
   dotnet restore E-Commerce.slnx
   ```
4. Update configuration values in `E-Commerce.Web/appsettings.json` or user secrets/environment variables.

## Configuration
Current configuration keys (from `E-Commerce.Web/appsettings.json`):
- `ConnectionStrings:DefaultConnection`
- `ConnectionStrings:IdentityConnection`
- `ConnectionStrings:RedisConnection`
- `Urls:BaseUrl`
- `JWTOptions:Issuer`
- `JWTOptions:Audience`
- `JWTOptions:Key`

> Security note: move secrets/connection details out of source-controlled `appsettings.json`.

## Database Setup
- Main EF Core context: `ApplicationDbContext`
- Identity context: `ApplicationIdentityDbContext`
- Migrations are present under:
  - `Infrastructure/Persistence/Data/Migrations`
  - `Infrastructure/Persistence/Identity/Migrations`
- Database and seed execution happen at app startup (`SeedDataAsync`).

Typical commands:
```bash
dotnet ef database update --project /tmp/workspace/A7med7c/Otlob-Backend/Infrastructure/Persistence/Persistence.csproj --startup-project /tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.Web/E-Commerce.Web.csproj --context ApplicationDbContext

dotnet ef database update --project /tmp/workspace/A7med7c/Otlob-Backend/Infrastructure/Persistence/Persistence.csproj --startup-project /tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.Web/E-Commerce.Web.csproj --context ApplicationIdentityDbContext
```

## Running the Project
```bash
dotnet run --project /tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.Web/E-Commerce.Web.csproj
```
Swagger UI is enabled in development.

## API Documentation
Main route prefix: `api/[controller]`

### Products
- `GET /api/Products`
- `GET /api/Products/{id}` (authorized)
- `GET /api/Products/brands`
- `GET /api/Products/types`

### Baskets
- `GET /api/Baskets/{key}`
- `POST /api/Baskets`
- `DELETE /api/Baskets/{key}`

### Orders
- `POST /api/Orders` (authorized)
- `GET /api/Orders` (authorized)
- `GET /api/Orders/{id}`
- `GET /api/Orders/DeliveryMethods`

### User
- `POST /api/User/login`
- `POST /api/User/register`
- `GET /api/User/CheckEmail`
- `GET /api/User` (authorized)
- `GET /api/User/address` (authorized)
- `PUT /api/User/address` (authorized)

## Authentication
- JWT bearer auth configured in `ServicesExtensions.AddJWTServices`
- Token claims: email, username, user id, and role claims
- Role data seeded during startup

## Project Structure
- `/Core` → domain + service contracts + service implementations
- `/Infrastructure` → persistence + presentation
- `/E-Commerce.Web` → startup host and middleware
- `/Shared` → DTOs and shared contracts

## Testing
No dedicated test projects are currently included.

Validation commands currently used:
```bash
dotnet build /tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.slnx
dotnet test /tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.slnx
```

## Deployment
No Dockerfiles or CI/CD workflow files are currently present in the repository. Deployment process is currently manual and environment-specific.

## Troubleshooting
- **DB connection errors**: verify SQL Server instance and connection strings.
- **Redis failures**: ensure Redis is running and `ConnectionStrings:RedisConnection` is valid.
- **401/403 issues**: verify JWT issuer/audience/key consistency and Authorization header formatting.
- **Runtime error on cached endpoints**: ensure caching services are registered (`ICashService` / `ICashRepository`).

## Future Improvements
- Externalize secrets/configuration
- Fix caching DI registration gap
- Add automated CI/CD and containerization
- Add test projects and quality gates
- Improve validation, observability, and security hardening

## Contributors
- Ahmed Ragab (120670728+A7med7c@users.noreply.github.com)
- Ahmed Ragab (ragabali@std.mans.edu.eg)

## License
No license file was found in the repository.
