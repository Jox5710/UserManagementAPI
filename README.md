# User Management API

A simple ASP.NET Core 9.0 API for managing users.  
Includes CRUD operations, input validation, and logging middleware.  
Swagger/OpenAPI is integrated for documentation and testing.

---

## Features

- **CRUD Endpoints** for Users
  - `GET /users` — List all users
  - `GET /users/{id}` — Get a user by ID
  - `POST /users` — Create a new user
  - `PUT /users/{id}` — Update an existing user
  - `DELETE /users/{id}` — Delete a user
- **Middleware Logging**: Logs request method and path to console.
- **Validation**: Ensures only valid user data is accepted.
- **Swagger/OpenAPI**: Accessible at `/swagger` for testing and documentation.

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

---

## Running the Project

1. Clone the repository:

```bash
git clone https://github.com/Jox5710/UserManagementAPI.git
cd UserManagementAPI
```
2. Restore dependencies and build the project:

```bash
dotnet restore
dotnet build
```
3.Run the application:
```bash
dotnet run
```
4.Open your browser and navigate to Swagger UI to test the API:
```bash
https://localhost:5001/swagger
```
## project structure
UserManagementAPI/
├── Controllers/
│   └── UserController.cs
├── Program.cs
├── UserManagementAPI.csproj
├── README.md
├── appsettings.json
├── appsettings.Development.json
├── Properties/
│   └── launchSettings.json
├── bin/
├── obj/
└── GeneratedApiClient.cs      (إذا استخدمت NSwag لتوليد العميل)


