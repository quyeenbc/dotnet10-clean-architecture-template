# .NET 10 Clean Architecture Template

Production-ready ASP.NET Core 10 template implementing Clean Architecture with CQRS, MediatR, FluentValidation, and PostgreSQL. Built as a reference architecture for enterprise .NET backend projects.

## Tech Stack

- **Runtime:** .NET 10 (ASP.NET Core 10)
- **Architecture:** Clean Architecture + CQRS + Domain-Driven Design
- **Messaging:** MediatR 14 with Pipeline Behaviors
- **Validation:** FluentValidation with automatic pipeline integration
- **Database:** PostgreSQL 16 via EF Core + Npgsql
- **Documentation:** Swagger / OpenAPI 3.0
- **Logging:** Serilog (Console + File)
- **Containerization:** Docker + Docker Compose

## Project Structure

```
├── CleanArchitecture.Domain          # Entities, Interfaces, Exceptions (no dependencies)
│   ├── Common/                       # BaseEntity, BaseAuditableEntity
│   ├── Entities/                     # Product, ...
│   ├── Interfaces/                   # IRepository, IUnitOfWork, IProductRepository
│   └── Exceptions/                   # DomainException, NotFoundException
│
├── CleanArchitecture.Application     # Business logic, CQRS handlers
│   ├── Common/Behaviors/             # ValidationBehavior (MediatR pipeline)
│   ├── Common/Models/                # Result<T> pattern
│   ├── Common/Interfaces/            # IApplicationDbContext
│   └── Features/Products/            # Commands & Queries (CQRS)
│       ├── Commands/CreateProduct/
│       └── Queries/GetProducts/
│
├── CleanArchitecture.Infrastructure  # EF Core, Repositories, external services
│   ├── Persistence/                  # ApplicationDbContext, EF Configurations
│   └── Repositories/                 # BaseRepository, ProductRepository
│
└── CleanArchitecture.API             # Controllers, Middleware, DI setup
    ├── Controllers/                  # ProductsController
    ├── Middleware/                   # Global exception handling
    └── Extensions/                  # Swagger, DI extensions
```

## Architecture Overview

```
API → Application → Domain ← Infrastructure
```

- **Domain** has zero dependencies
- **Application** depends only on Domain
- **Infrastructure** implements Application interfaces
- **API** wires everything together via DI

## Key Patterns

| Pattern | Implementation |
|---|---|
| CQRS | MediatR Commands & Queries |
| Repository | Generic `IRepository<T>` + specific repositories |
| Unit of Work | `IUnitOfWork` implemented by `ApplicationDbContext` |
| Result Pattern | `Result<T>` — no exception throwing for business errors |
| Validation Pipeline | FluentValidation auto-runs via MediatR `IPipelineBehavior` |
| Global Error Handling | Custom middleware with structured JSON error response |

## Getting Started

### Prerequisites
- .NET 10 SDK
- Docker Desktop

### Run with Docker

```bash
# Start PostgreSQL
docker run --name postgres-dev \
  -e POSTGRES_PASSWORD=Admin@123 \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_DB=CleanArchDb \
  -p 5432:5432 -d postgres:16

# Apply migrations
dotnet ef database update \
  --project CleanArchitecture.Infrastructure \
  --startup-project CleanArchitecture.API

# Run API
dotnet run --project CleanArchitecture.API
```

### Swagger UI
```
http://localhost:5027/swagger
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | Get all products |
| POST | `/api/products` | Create a new product |

### Create Product — Request Body
```json
{
  "name": "Sample Product",
  "description": "Product description",
  "price": 29.99,
  "stock": 100
}
```

## Connection String

Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=CleanArchDb;Username=postgres;Password=Admin@123"
  }
}
```

## Author

**Banh Cao Quyen** — Senior .NET Backend Engineer  
17+ years experience in ASP.NET Core, Microservices, DDD, Azure  
📧 quyeenbc@gmail.com  
🔗 [GitHub](https://github.com/quyeenbc)

## License

MIT