# Otlob Backend API

A production-oriented E-Commerce Backend built with ASP.NET Core Web API following Onion Architecture principles and modern software engineering practices.

The project provides a complete backend solution for managing products, baskets, orders, payments, authentication, authorization, notifications, caching, and file storage. It demonstrates enterprise-level backend development concepts including clean architecture, domain-driven design principles, scalable application structure, and integration with external services.

---

## Table of Contents

* Overview
* Architecture
* Key Features
* Technology Stack
* Design Patterns
* Authentication Flow
* Payment Flow
* API Modules
* Product Filtering & Pagination
* Project Structure
* Getting Started
* Configuration
* Database & Seeding
* Future Improvements
* Author

---

## Overview

Otlob Backend API is designed to simulate a real-world e-commerce platform capable of handling authentication, product management, shopping baskets, order processing, payment workflows, notifications, and caching.

The application emphasizes:

* Clean separation of concerns
* Maintainable and scalable architecture
* Secure authentication and authorization
* Performance optimization through Redis caching
* Extensible business logic through specifications
* Integration with third-party services

---

## Architecture

The project follows Onion Architecture to ensure that business logic remains independent from infrastructure concerns.

```text
Client Applications
(Angular, React, Mobile Apps, Postman)
                │
                ▼
         ASP.NET Core API
                │
                ▼
        Application Layer
        Services & DTOs
                │
                ▼
          Domain Layer
   Entities & Business Rules
                │
                ▼
      Infrastructure Layer
Database, Redis, Identity,
Stripe, Twilio, SMTP
```

### Architectural Goals

* High Maintainability
* Separation of Concerns
* Testability
* Scalability
* Loose Coupling
* Extensibility

---

## Key Features

### Authentication & Authorization

* ASP.NET Core Identity
* JWT Authentication
* Refresh Token Mechanism
* Role-Based Authorization
* Custom Authorization Attributes
* Email Confirmation
* Secure Login & Registration
* User Profile Management

### Product Management

* Product CRUD Operations
* Product Brands
* Product Types
* Product Search
* Dynamic Filtering
* Sorting
* Pagination

### Basket Management

* Create Basket
* Update Basket
* Retrieve Basket
* Delete Basket
* Redis Basket Storage

### Order Management

* Create Orders
* Retrieve User Orders
* Order Details
* Delivery Methods

### Payments

* Stripe Payment Gateway Integration
* Payment Intent Creation
* Payment Confirmation
* Webhook Processing

### Notifications

* Gmail SMTP Integration
* Twilio SMS Integration

### Caching

* Redis Distributed Cache
* Basket Persistence
* Performance Optimization

### Error Handling

* Global Exception Middleware
* Custom Exceptions
* Unified API Responses

### Data Management

* Database Seeding
* Identity Seeding
* Role Seeding

---

## Technology Stack

### Backend

* ASP.NET Core Web API (.NET 10)
* Entity Framework Core
* SQL Server

### Authentication

* ASP.NET Core Identity
* JWT Bearer Authentication
* Refresh Tokens

### Caching

* Redis
* StackExchange.Redis

### Payments

* Stripe API

### Notifications

* Gmail SMTP
* Twilio

### Object Mapping

* AutoMapper

### Documentation

* Swagger / OpenAPI

---

## Design Patterns

### Onion Architecture

Ensures a clean separation between business logic and infrastructure concerns.

### Repository Pattern

Provides abstraction over data access operations.

### Generic Repository Pattern

Reduces duplication by implementing reusable CRUD operations.

### Unit Of Work Pattern

Coordinates repository operations and transaction management.

### Specification Pattern

Used to support:

* Dynamic Filtering
* Searching
* Sorting
* Pagination
* Query Reusability

### Dependency Injection

Provides loose coupling and improved testability.

---

## Authentication Flow

```text
User Registration
        │
        ▼
Email Confirmation
        │
        ▼
User Login
        │
        ▼
Generate JWT Token
Generate Refresh Token
        │
        ▼
Access Protected Resources
        │
        ▼
Refresh Access Token When Expired
```

---

## Payment Flow

```text
User Checkout
       │
       ▼
Create Payment Intent
       │
       ▼
Stripe
       │
       ▼
Payment Confirmation
       │
       ▼
Stripe Webhook
       │
       ▼
Order Status Update
       │
       ▼
Email Notification
SMS Notification
```

---

## API Modules

### Authentication

* User Registration
* User Login
* Refresh Token
* Logout
* Email Confirmation
* User Profile

### Products

* Products
* Product Brands
* Product Types

### Basket

* Create Basket
* Update Basket
* Retrieve Basket
* Delete Basket

### Orders

* Create Orders
* Retrieve Orders
* Retrieve Order Details
* Delivery Methods

### Payments

* Create Payment Intent
* Handle Stripe Webhooks

### Notifications

* Email Notifications
* SMS Notifications

---

## Product Filtering & Pagination

The project implements the Specification Pattern to provide flexible and reusable query logic.

Example:

```http
GET /api/products?search=iphone&brandId=1&typeId=2&sort=priceDesc&pageIndex=1&pageSize=10
```

Supported Operations:

* Search
* Filtering
* Sorting
* Pagination

---

## Project Statistics

* 30+ API Endpoints
* Onion Architecture
* ASP.NET Identity Integration
* JWT & Refresh Tokens
* Stripe Payment Integration
* Redis Distributed Cache
* SMTP Email Service
* SMS Notifications
* Specification Pattern
* AutoMapper
* Global Exception Handling
* Database Seeding

---

## Project Structure

```text
Otlob-Backend
│
├── Core
│   ├── DomainLayer
│   ├── ServiceLayer
│   └── ServiceImplementation
│
├── Infrastructure
│   ├── Persistence
│   └── PresentationLayer
│
├── Shared
│   ├── DTOs
│   ├── Responses
│   └── Common Models
│
├── E-Commerce.Web
│   ├── Middleware
│   ├── Configuration
│   ├── Extensions
│   └── Program.cs
│
└── README.md
```

---

## Getting Started

### Prerequisites

* .NET 10 SDK
* SQL Server
* Redis Server
* Stripe Account
* Twilio Account

### Clone Repository

```bash
git clone https://github.com/A7med7c/Otlob-Backend.git
cd Otlob-Backend
```

### Restore Dependencies

```bash
dotnet restore
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run --project E-Commerce.Web
```

---

## Configuration

Configure the following settings through `appsettings.json`, environment variables, or user secrets.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "",
    "IdentityConnection": "",
    "RedisConnection": ""
  },

  "JWTOptions": {
    "Issuer": "",
    "Audience": "",
    "Key": ""
  },

  "Stripe": {
    "SecretKey": "",
    "PublishableKey": ""
  },

  "Twilio": {
    "AccountSid": "",
    "AuthToken": "",
    "PhoneNumber": ""
  },

  "EmailSettings": {
    "Email": "",
    "Password": ""
  }
}
```

---

## Database & Seeding

The application automatically performs:

* Database Migration
* Product Seeding
* Product Brand Seeding
* Product Type Seeding
* Identity Seeding
* Roles Seeding

---

## Future Improvements

* Docker Support
* CI/CD Pipelines
* Unit Testing
* Integration Testing
* Serilog Logging
* OpenTelemetry Monitoring
* Rate Limiting
* Background Jobs with Hangfire

---

## Author

Ahmed Ragab

Fullstach .NET & Angular Developer

GitHub:
https://github.com/A7med7c

---

This project demonstrates enterprise-level backend development practices including Onion Architecture, Specification Pattern, JWT Authentication, Refresh Tokens, Redis Caching, Stripe Payments, ASP.NET Identity, and scalable API design.
