🚀 Quick Start/Run Checklist
## Backend (.NET Core) - Clean Architecture

- backend
  - backend.Domain       # Entities, Enums, Interfaces (Core Layer)
  - backend.Application  # DTOs, Services, Business Logic
  - backend.Infrastructure # EF Core, Database Context, Repositories
  - backend.API          # Controllers, Endpoints, Program.cs


##### 1. Start SQL Server (local or Docker)

## Prerequisites

- .NET 8 SDK
- SQL Server


## 🔧 Setup & Run Instructions

### Backend (.NET Core API)

1. **Apply migrations and create database**
```bash
dotnet ef database update --project backend.Infrastructure --startup-project backend.API
```
2. **Navigate to the API project**
```bash
cd backend.API
```
3. **Run the API**
```bash
dotnet run
```

**The API will run at: http://localhost:5274**
**Swagger docs: http://localhost:5274/swagger**

### Client (Angular)
  1. **Navigate to the Angular project:**
  ```bash
   cd client
  ```
  2. **Install dependencies:**
  ```bash
   npm install
  ```
  3. **Start Angular development server:**
  ```bash
   ng serve
  ```

## Useful Links
  API Swagger: http://localhost:5274/swagger
  Video demo: [Insert your video link here]

## Notes
  Make sure SQL Server is running (or in Docker) before starting the API.
  Adjust ports in appsettings.json or environment.ts if needed.
