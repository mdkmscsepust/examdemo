🚀 Quick Start/Run Checklist

1. Start SQL Server (local or Docker)
2. Backend (.NET Core) - Clean Architecture:
  /backend
    /backend.Domain       
    /backend.Application
    /backend.Infrastructure
    /backend.API
3. Prerequisites
    .NET 8 SDK
    SQL Server
4. 🔧 Setup & Run Instructions
    Apply migrations and create database:
     dotnet ef database update --project backend.Infrastructure --startup-project backend.API
   Navigate to the API project:
     cd backend.API
  Run the API:
    dotnet run
5. Client (Angular)
  Navigate to the Angular project:
    cd client
  Install dependencies:
    npm install
  Start Angular development server:
    ng serve
6. Useful Links
  API Swagger: http://localhost:5274/swagger
  Video demo: [Insert your video link here]
7. Notes
  Make sure SQL Server is running (or in Docker) before starting the API.
  Adjust ports in appsettings.json or environment.ts if needed.
