# CfCourseManagementSystem

A full-stack Course Management System built with ASP.NET Core Web API and Angular, designed to manage courses, teachers, students, and enrollments with authentication and role-based authorization.

---

## Features

### Backend (ASP.NET Core Web API)
- RESTful API architecture
- Entity Framework Core (Code First)
- SQL Server LocalDB
- JWT Authentication
- Role-based Authorization (Admin / Teacher / Student)
- CRUD operations for Courses, Teachers, Students
- Course enrollments (many-to-many)
- Proper HTTP status codes & error handling
- Swagger API documentation

### Frontend (Angular)
- Standalone Angular application
- Authentication (Login / Register)
- JWT token handling
- Protected routes (Guards)
- CRUD UI for Courses, Teachers, Students
- Enrollment management UI
- Clean academic-style UI

---

## Tech Stack

### Backend
- ASP.NET Core (.NET 8)
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication

### Frontend
- Angular
- TypeScript
- SCSS
- RxJS

---

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- Node.js (LTS)
- Angular CLI
- SQL Server LocalDB

---

## Backend Setup

```bash
cd CfCourseManagement
cd CfCourseManagement.Api
dotnet restore
dotnet ef database update
dotnet run

API will be available at:
https://localhost:7286/swagger
```

---

## Frontend Setup

```bash
cd cms-frontend
npm ci
ng serve

Frontend will be available at:
http://localhost:4200
```

---

## Notes
- appsettings.Development.json is excluded from git
- Secrets (connection strings, JWT keys) are not committed
- node_modules, bin, obj, .vs folders are ignored
- The project runs locally without extra configuration

---

## Project Status
- Backend complete
- Frontend complete
- Authentication & Authorization implemented
- Database migrations applied
- Ready for GitHub & portfolio
