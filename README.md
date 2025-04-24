# University Management System

The University Management System aims to digitize and streamline all core administrative operations of an educational institution. The system will serve as a comprehensive platform to manage students, faculty, courses, classes, departments, and physical resources while facilitating efficient information flow between all stakeholders.

## Overview

The University Management System is designed to efficiently manage various aspects of an educational institution, including:

- Student enrollment and records
- Course and class management
- Instructor management
- Office and section administration
- Academic scheduling

This project demonstrates best practices in modern software architecture using ASP.NET Core, Entity Framework Core, and RESTful API design.

## Architecture

This application follows Clean Architecture principles with a clear separation of concerns across four layers:

### Domain Layer

- Core business entities (Student, Course, Instructor, etc.)
- Domain logic and rules
- No dependencies on other layers or frameworks

### Application Layer

- Business use cases and orchestration
- Interface definitions for external dependencies
- DTOs (Data Transfer Objects)
- Service interfaces and implementations

### Infrastructure Layer

- Data access implementation using EF Core
- External service integrations
- Repository implementations
- Framework-specific concerns

### API Layer (Presentation)

- REST API endpoints
- Request/response handling
- Dependency registration
- Middleware configuration

## Technical Stack

- **ASP.NET Core 7.0**: Web API framework
- **Entity Framework Core**: ORM for database operations
- **PostgreSQL**: Primary database
- **Dependency Injection**: Native ASP.NET Core DI container
- **Swagger**: API documentation

## Getting Started

### Prerequisites

- .NET 7.0 SDK or later
- PostgreSQL
- Visual Studio 2022 or other preferred IDE

### Setup Instructions

1. Clone the repository
2. Update the connection string in `appsettings.json` to point to your PostgreSQL instance
3. Apply database migrations
4. Run the application

## Project Structure

```
UniversityManagementSystem/
├── src/
│   ├── UniversityManagementSystem.Domain/
│   │   ├── Entities/
│   │   └── Enums/
│   ├── UniversityManagementSystem.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   │   ├── Repositories/
│   │   │   └── Services/
│   │   ├── Services/
│	│	└── DependencyInjection.cs
│   ├── UniversityManagementSystem.Infrastructure/
│   │   ├── Data/
│   │   ├── Repositories/
│   │   └── DependencyInjection.cs
│   └── UniversityManagementSystem.API/
│       ├── Controllers/
│       ├── Middleware/
│       └── Program.cs
└── tests/
    ├── UniversityManagementSystem.UnitTests/
    └── UniversityManagementSystem.IntegrationTests/
```

## API Endpoints

The API provides comprehensive endpoints for managing all aspects of the university system. Access the Swagger documentation for detailed API information.

Main endpoint categories:

- `/api/students` - Student management
- `/api/courses` - Course operations
- `/api/instructors` - Instructor management
- `/api/classes` - Class scheduling and assignment
- `/api/sections` - Department and section management
- `/api/offices` - Office and facility management

## Architecture Highlights

- **Clean Architecture**: Clear separation of concerns with dependencies pointing inward
- **Domain-Driven Design**: Focus on the core business domain
- **Repository Pattern**: Abstraction over data access
- **SOLID Principles**: Focus on creating maintainable and extendable code
- **Dependency Injection**: Loose coupling between components

## Learning Objectives

This project was created to demonstrate and learn:

5. Clean Architecture patterns and principles
6. RESTful API design best practices
7. Entity Framework Core for data access
8. Dependency Injection in ASP.NET Core
9. Service and repository patterns
10. Domain-driven design concepts

## Future Enhancements

- Authentication and authorization using JWT tokens
- Pagination, filtering, and sorting for all list endpoints
- Advanced reporting features
- Frontend application using Angular or React
- Background processing for scheduled tasks

## License

This project is licensed under the [MIT License](./LICENSE).


