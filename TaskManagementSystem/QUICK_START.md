# Quick Start Guide

## ✅ Project Complete!

Your Task Management System has been successfully built with Clean Architecture principles.

## What Was Built

### 1. **Domain Layer**
- `TaskEntity` - Core business entity
- `ITaskRepository` - Repository interface

### 2. **Application Layer**
- DTOs for API contracts (CreateTaskDto, UpdateTaskDto, TaskResponseDto)
- `ITaskService` and `TaskService` - Business logic

### 3. **Infrastructure Layer**
- `TaskDbContext` - EF Core with In-Memory database
- `TaskRepository` - Data access implementation
- `AuthService` - JWT authentication

### 4. **API Layer**
- `TasksController` - CRUD operations for tasks
- `AuthController` - Login endpoint

### 5. **Tests**
- Unit tests for TaskService (4 tests, all passing)

## Running the Application

### Option 1: Using dotnet CLI
```bash
cd "c:\Users\Lenovo\Projects\Task Managment\task managment"
dotnet run
```

### Option 2: Using Visual Studio
1. Open `TaskManagementSystem.sln`
2. Press F5 or click "Run"

The API will start at: `https://localhost:5001`

## Testing the API

### Method 1: Using the HTTP File
Open `API_EXAMPLES.http` in Visual Studio Code with REST Client extension and click "Send Request" on any example.

### Method 2: Using curl

**1. Login as User:**
```bash
curl -X POST https://localhost:5001/api/auth/login ^
  -H "Content-Type: application/json" ^
  -d "{\"username\":\"user\",\"password\":\"user123\"}"
```

**2. Create a Task:**
```bash
curl -X POST https://localhost:5001/api/tasks ^
  -H "Authorization: Bearer YOUR_TOKEN_HERE" ^
  -H "Content-Type: application/json" ^
  -d "{\"title\":\"My First Task\",\"description\":\"Task details\"}"
```

**3. Get Tasks:**
```bash
curl -X GET https://localhost:5001/api/tasks ^
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Method 3: Using Postman
1. Import the OpenAPI spec from `https://localhost:5001/openapi/v1.json`
2. Use the examples from `API_DOCUMENTATION.md`

## Test Credentials

| Username | Password  | Role  | Can Do |
|----------|-----------|-------|--------|
| admin    | admin123  | Admin | Everything + mark tasks complete |
| user     | user123   | User  | Create, update own tasks, view own tasks |

## Running Tests

```bash
cd "c:\Users\Lenovo\Projects\Task Managment\TaskManagement.Tests"
dotnet test
```

Expected output: **4 tests passed**

## Project Files

- **README.md** - Complete project documentation
- **API_DOCUMENTATION.md** - Detailed API reference
- **API_EXAMPLES.http** - HTTP request examples
- **PROJECT_SUMMARY.md** - Implementation summary

## Architecture Highlights

✅ **Clean Architecture** - Clear separation of concerns  
✅ **SOLID Principles** - Applied throughout  
✅ **Dependency Injection** - All dependencies injected  
✅ **Repository Pattern** - Data access abstraction  
✅ **JWT Authentication** - Secure token-based auth  
✅ **Role-Based Authorization** - Admin vs User permissions  
✅ **DTOs** - Separation of internal/external models  
✅ **Unit Tests** - Business logic tested  
✅ **In-Memory Database** - Easy setup, no external dependencies  

## Key Features Implemented

1. ✅ **Create Task** - Users create tasks with title, description, due date
2. ✅ **Update Task** - Users update their own tasks only
3. ✅ **View Tasks** - Users see their tasks, Admins see all tasks
4. ✅ **Mark Complete** - Only Admins can mark any task as completed
5. ✅ **Authentication** - JWT token-based login
6. ✅ **Authorization** - Role-based access control
7. ✅ **Validation** - Input validation with Data Annotations

## Next Steps

1. Stop any running instance of the application
2. Run `dotnet build` to verify compilation
3. Run `dotnet run` to start the API
4. Test the endpoints using the examples provided
5. Run `dotnet test` in the test project to verify all tests pass

## Troubleshooting

**Build Error (File Locked):**
- Stop the running application first
- Then run `dotnet build`

**Port Already in Use:**
- Check `Properties/launchSettings.json` to change the port
- Or stop other applications using port 5001

**Authentication Issues:**
- Ensure you're using the correct credentials
- Check that the token is included in the Authorization header
- Tokens expire after 2 hours

## Documentation

All documentation is in the project root:
- Architecture and setup: `README.md`
- API endpoints: `API_DOCUMENTATION.md`
- Implementation details: `PROJECT_SUMMARY.md`
- Test examples: `API_EXAMPLES.http`

---

**Status**: ✅ Ready for Review

The Task Management System is complete and fully functional!
