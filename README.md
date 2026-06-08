# Otlob Backend API

## Overview

Otlob Backend API is a scalable e-commerce backend built using ASP.NET Core Web API and Onion Architecture principles. The project provides a complete backend solution for managing products, baskets, orders, payments, user authentication, notifications, caching, and file storage.

The application follows clean separation of concerns using Repository Pattern, Unit of Work Pattern, Specification Pattern, and Dependency Injection to ensure maintainability, scalability, and testability.

---

## Features

### Authentication & Authorization

* User Registration
* User Login
* JWT Authentication
* Refresh Token Authentication
* ASP.NET Core Identity Integration
* Role-Based Authorization
* Custom Authorization Attributes
* Email Confirmation
* Logout Functionality
* Email Availability Validation

### Product Management

* Product CRUD Operations
* Product Brand Management
* Product Type Management
* Product Search
* Product Filtering
* Product Sorting
* Pagination Support
* Resource URL Resolution

### Basket Management

* Create Basket
* Update Basket
* Retrieve Basket
* Delete Basket
* Redis-Based Basket Storage

### Order Management

* Create Orders
* Retrieve User Orders
* Retrieve Order Details
* Delivery Methods
* Order Item Management

### Payment Processing

* Stripe Payment Integration
* Payment Intent Creation
* Payment Webhooks
* Payment Status Updates

### Notifications

* Email Notifications using Gmail SMTP
* SMS Notifications using Twilio

### File Storage

* File Storage Service
* Resource Management

### Caching

* Redis Distributed Cache
* Basket Caching
* Custom Caching Services

### Error Handling

* Global Exception Handling Middleware
* Custom Exceptions
* Validation Error Responses
* Unified API Response Structure

### Data Management

* Database Seeding
* Identity Seeding
* Roles Seeding

---

## Architecture

The project follows Onion Architecture to maintain a clear separation between business logic, application logic, infrastructure, and presentation layers.

### Layers

#### Domain Layer

Contains:

* Entities
* Contracts
* Domain Exceptions

#### Application Layer

Contains:

* Service Contracts
* Specifications
* Business Logic

#### Infrastructure Layer

Contains:

* Entity Framework Core
* SQL Server Integration
* ASP.NET Identity
* Redis Integration
* Stripe Integration
* Twilio Integration
* SMTP Services
* Repositories
* Unit Of Work

#### Presentation Layer

Contains:

* API Controllers
* Filters
* Middleware
* Dependency Injection Configuration

---

## Design Patterns

### Repository Pattern

Provides abstraction over data access operations.

### Generic Repository Pattern

Reusable CRUD functionality across entities.

### Unit Of Work Pattern

Coordinates repositories and manages transactions.

### Specification Pattern

Used for:

* Filtering
* Searching
* Sorting
* Pagination
* Navigation Property Includes

### Dependency Injection

Provides loose coupling and testability.

### Onion Architecture

Ensures separation of concerns and maintainable code structure.

---

## Technologies

### Backend

* ASP.NET Core Web API (.NET 10)
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity

### Authentication

* JWT Bearer Authentication
* Refresh Tokens
* Role-Based Authorization

### Caching

* Redis
* StackExchange.Redis

### Payments

* Stripe API

### Notifications

* Gmail SMTP
* Twilio SMS API

### Object Mapping

* AutoMapper

### Documentation

* Swagger / OpenAPI

---

## API Endpoints

### Authentication

| Method | Endpoint                      |
| ------ | ----------------------------- |
| POST   | /api/auth/register            |
| POST   | /api/auth/login               |
| POST   | /api/auth/refresh-token       |
| POST   | /api/auth/logout              |
| GET    | /api/auth                     |
| GET    | /api/auth/email-exists        |
| GET    | /api/auth/confirm-email       |
| POST   | /api/auth/resend-confirmation |

### Address

| Method | Endpoint          |
| ------ | ----------------- |
| GET    | /api/auth/address |
| PUT    | /api/auth/address |

### Products

| Method | Endpoint             |
| ------ | -------------------- |
| GET    | /api/products        |
| GET    | /api/products/{id}   |
| POST   | /api/products        |
| PUT    | /api/products/{id}   |
| DELETE | /api/products/{id}   |
| GET    | /api/products/brands |
| GET    | /api/products/types  |

### Basket

| Method | Endpoint           |
| ------ | ------------------ |
| GET    | /api/baskets       |
| POST   | /api/baskets       |
| DELETE | /api/baskets       |
| GET    | /api/baskets/{key} |
| DELETE | /api/baskets/{key} |

### Orders

| Method | Endpoint                    |
| ------ | --------------------------- |
| POST   | /api/orders                 |
| GET    | /api/orders                 |
| GET    | /api/orders/{id}            |
| GET    | /api/orders/deliveryMethods |

### Payments

| Method | Endpoint                 |
| ------ | ------------------------ |
| POST   | /api/payments/{basketId} |
| POST   | /api/payments/webhook    |

### Notifications

| Method | Endpoint                      |
| ------ | ----------------------------- |
| POST   | /api/notifications/send-email |
| POST   | /api/notifications/send-sms   |

---

## Product Search, Filtering, Sorting and Pagination

The project implements the Specification Pattern to support advanced querying capabilities.

Example:

```http
GET /api/products?search=iphone&brandId=1&typeId=2&sort=priceDesc&pageIndex=1&pageSize=10
```

Supported features:

* Search by product name
* Filter by brand
* Filter by type
* Sort by price ascending
* Sort by price descending
* Pagination

---

## Database

### Main Context

```csharp
ApplicationDbContext
```

### Identity Context

```csharp
ApplicationIdentityDbContext
```

### Database Provider

```text
SQL Server
```

### Startup Initialization

The application automatically:

* Applies migrations
* Seeds products
* Seeds brands
* Seeds product types
* Seeds roles
* Seeds identity data

---

## Redis Usage

Redis is used for:

* Basket storage
* Distributed caching
* Performance optimization

---

## Stripe Integration

Implemented functionality includes:

* Create Payment Intent
* Update Payment Intent
* Payment Confirmation
* Webhook Handling
* Order Payment Tracking

---

## Notification Services

### Email

Implemented using Gmail SMTP.

Use cases:

* Email Confirmation
* Business Notifications

### SMS

Implemented using Twilio.

Use cases:

* User Notifications
* Order Updates

---

## Swagger Documentation

Swagger/OpenAPI documentation is available during development for testing and API exploration.

```
https://localhost:{port}/swagger
```

---

## Project Highlights

* Onion Architecture
* ASP.NET Core Identity
* JWT Authentication
* Refresh Tokens
* Role-Based Authorization
* Custom Authorization Attributes
* Repository Pattern
* Generic Repository Pattern
* Unit Of Work Pattern
* Specification Pattern
* AutoMapper
* Redis Caching
* Stripe Payments
* Twilio SMS Integration
* Gmail SMTP Integration
* Global Exception Middleware
* Custom Exceptions
* Database Seeding
* Swagger Documentation
* File Storage Service

---

## Future Improvements

* Docker Support
* CI/CD Pipelines
* Unit Testing
* Integration Testing
* Rate Limiting
* Structured Logging with Serilog
* OpenTelemetry Monitoring
* Background Jobs using Hangfire

---

## Author

Ahmed Ragab

Backend Developer

This project demonstrates enterprise-level backend development concepts including Onion Architecture, Specification Pattern, JWT Authentication, Refresh Tokens, Redis Caching, Stripe Payments, ASP.NET Identity, and scalable API design.
