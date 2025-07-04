# BsStoreApp - RESTful Web API

[![.NET](https://img.shields.io/badge/.NET-6.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/6.0)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-7.0.3-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## 📖 Overview

BsStoreApp is a comprehensive RESTful Web API built with .NET 6 that demonstrates modern web API development practices. The application serves as a book store management system with full CRUD operations, advanced features like pagination, filtering, caching, versioning, and HATEOAS (Hypermedia as the Engine of Application State) implementation.

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
├── WebApi/              # Entry point and API configuration
├── Presentation/        # Controllers and API presentation layer
├── Services/           # Business logic and service implementations
├── Repositories/       # Data access layer with Entity Framework
└── Entities/          # Domain models, DTOs, and shared contracts
```

### Key Architectural Patterns

- **Repository Pattern**: Abstracts data access logic
- **Service Layer Pattern**: Encapsulates business logic
- **Dependency Injection**: Promotes loose coupling and testability
- **DTO Pattern**: Separates internal models from API contracts
- **CQRS-like approach**: Separate DTOs for different operations

## ✨ Features

### Core Functionality
- 📚 **Complete Book Management**: Create, Read, Update, Delete operations
- 🔍 **Advanced Filtering**: Filter books by various criteria
- 📄 **Pagination**: Efficient data retrieval with metadata
- 🔗 **HATEOAS**: Hypermedia-driven API responses
- 📝 **Partial Updates**: JSON Patch support for efficient updates

### Technical Features
- 🚀 **API Versioning**: Multiple API versions support
- 💾 **Response Caching**: Improved performance with caching strategies
- 📊 **Logging**: Comprehensive logging with NLog
- 🔒 **CORS**: Cross-origin resource sharing configuration
- 📋 **Data Validation**: Robust input validation with action filters
- 🎯 **Content Negotiation**: Multiple response formats (JSON, XML, CSV)
- 📈 **HTTP Cache Headers**: Client-side caching optimization

## 🛠️ Technology Stack

### Backend Technologies
- **.NET 6**: Latest LTS version of .NET
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core 7.0.3**: Object-relational mapping
- **SQL Server LocalDB**: Database engine
- **AutoMapper 12.0.0**: Object-to-object mapping
- **NLog 5.1.2**: Logging framework

### Key NuGet Packages
- **Microsoft.EntityFrameworkCore**: Data access
- **Microsoft.AspNetCore.JsonPatch**: JSON Patch support
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson**: JSON serialization
- **Microsoft.AspNetCore.Mvc.Versioning**: API versioning
- **System.Linq.Dynamic.Core**: Dynamic LINQ queries
- **Marvin.Cache.Headers**: HTTP cache headers

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/BsStoreApp.git
   cd BsStoreApp
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update database connection string** (if needed)
   
   Edit `WebApi/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "sqlConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bsStoreApp;Integrated Security=true;"
     }
   }
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update --project WebApi
   ```

5. **Run the application**
   ```bash
   dotnet run --project WebApi
   ```

6. **Access the API**
   - API Base URL: `https://localhost:7001` or `http://localhost:5001`
   - Swagger UI: `https://localhost:7001/swagger`

## 📚 API Documentation

### Base Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/books` | Get all books with pagination |
| GET | `/api/books/{id}` | Get a specific book |
| POST | `/api/books` | Create a new book |
| PUT | `/api/books/{id}` | Update a book |
| PATCH | `/api/books/{id}` | Partially update a book |
| DELETE | `/api/books/{id}` | Delete a book |
| OPTIONS | `/api/books` | Get allowed HTTP methods |
| HEAD | `/api/books` | Get headers without body |

### Query Parameters

- **Pagination**: `pageNumber`, `pageSize`
- **Filtering**: `minPrice`, `maxPrice`, `searchTerm`
- **Sorting**: `orderBy`
- **Field Selection**: `fields`

### Example Requests

#### Get Books with Pagination
```http
GET /api/books?pageNumber=1&pageSize=10&minPrice=10&maxPrice=100
```

#### Create a New Book
```http
POST /api/books
Content-Type: application/json

{
  "title": "Clean Code",
  "price": 45.99
}
```

#### Partial Update with JSON Patch
```http
PATCH /api/books/1
Content-Type: application/json-patch+json

[
  {
    "op": "replace",
    "path": "/price",
    "value": 39.99
  }
]
```

## 🔧 Configuration

### Logging Configuration

Logging is configured via `nlog.config`:
- Log files are stored in `./logs/` directory
- Internal logs are stored in `./internal_logs/`
- Configurable log levels and targets

### Caching Configuration

- **Response Caching**: 5-minute cache profile for GET requests
- **HTTP Cache Headers**: 80-second max-age with public cache location
- **Cache Validation**: Configurable revalidation settings

### CORS Configuration

- Allows all origins, methods, and headers
- Exposes `X-Pagination` header for pagination metadata

## 🧪 Testing

### Using Swagger UI
1. Navigate to `https://localhost:7001/swagger`
2. Explore and test all available endpoints
3. View request/response schemas

### Using Postman
1. Import the API collection (if available)
2. Set base URL to `https://localhost:7001`
3. Test various endpoints with different parameters

## 📁 Project Structure

```
BsStoreApp/
├── Entities/
│   ├── DTOs/                    # Data Transfer Objects
│   ├── Models/                  # Domain Models
│   ├── Exceptions/              # Custom Exceptions
│   ├── RequestFeatures/         # Pagination and Filtering
│   └── LinkModels/              # HATEOAS Link Models
├── Repositories/
│   ├── Contracts/               # Repository Interfaces
│   └── EFCore/                  # Entity Framework Implementations
├── Services/
│   ├── Contracts/               # Service Interfaces
│   └── *.cs                     # Service Implementations
├── Presentation/
│   ├── Controllers/             # API Controllers
│   └── ActionFilters/           # Custom Action Filters
└── WebApi/
    ├── Extensions/              # Service Extensions
    ├── Utilities/               # Helper Classes
    └── Program.cs               # Application Entry Point
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Ahmet Coşkun Kızılkaya**

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- RESTful API design best practices
- ASP.NET Core community and documentation
- Entity Framework Core team

---

⭐ If you found this project helpful, please give it a star!