# 📝 HR.LeaveManagement — ASP.NET Core Clean Architecture

A modular, scalable ASP.NET Core Web API project for managing employee leave requests, following **Clean Architecture**, **CQRS (Command Query Responsibility Segregation)**, and **MediatR** for cleanly separating concerns, promoting testability, and supporting maintainable enterprise-grade systems.

> 🔗 [Live Repository](https://github.com/GioAbuladze-Creator/ASP.NET-Core-Clean-Architecture)

---

## 📖 About The Project

This system enables management of:
- ✅ Leave Types
- ✅ Leave Allocations
- ✅ Leave Requests

Built with:
- Clean Architecture structure (Domain, Application, Persistence, API)
- CQRS pattern using **MediatR**
- **Entity Framework Core** for database access
- **FluentValidation** for request validation
- **AutoMapper** for object mapping
- **SendGrid** integration for sending emails
- API project configured as the Startup Project

---

## 🏛️ Clean Architecture Overview

This project applies **Robert C. Martin’s Clean Architecture** pattern:

- **Domain Layer:** Core business entities and domain logic.
- **Application Layer:** Use cases (commands/queries), DTOs, service interfaces, and AutoMapper profiles.
- **Infrastructure Layer:** Interfaces for external dependencies (e.g. email service, logging).
- **Persistence Layer:** Entity Framework Core implementation and database migrations.
- **API Layer (Presentation):** Web API controllers, request handling, and API-specific models.

---

![API_Diagram](https://github.com/user-attachments/assets/283ce70a-5b2e-431e-823c-aa20863e178b)


## 📊 CQRS with MediatR

This project uses **MediatR** to implement the **CQRS** pattern:

- **Commands** handle actions that change state (e.g. `CreateLeaveRequestCommand`).
- **Queries** retrieve data without modifying the state (e.g. `GetLeaveTypeListRequest`).
- Handlers process these requests using dependency injection and MediatR pipelines.
- MediatR promotes decoupled, testable code by handling cross-cutting concerns separately.

**Command Example:**

POST /api/LeaveRequests

→ MediatR Command: CreateLeaveRequestCommand

→ Handler: CreateLeaveRequestCommandHandler

→ Returns: Result (Base command response)

---

## 🚀 Getting Started

### 📦 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### ⚙️ Setup Instructions

1️⃣ **Clone the repository**
```bash
   git clone https://github.com/GioAbuladze-Creator/ASP.NET-Core-Clean-Architecture.git
   
   cd ASP.NET-Core-Clean-Architecture

```

2️⃣ **Apply Database Migrations**
```bash

   dotnet ef database update --project HR.LeaveManagement.Persistence --startup-project HR.LeaveManagement.API

```

3️⃣ **Run the API Project**
```bash

   dotnet run --project HR.LeaveManagement.API

```





