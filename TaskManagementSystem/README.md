# Task Management System API

A simplified Task Management System built with ASP.NET Core Web API following Clean Architecture principles. This system allows authenticated users to manage their tasks with role-based access control.

## Architecture

The project follows **Clean Architecture** with clear separation of concerns:

```
├── Domain/                    # Core business entities and interfaces
│   ├── TaskEntity.cs         # Task domain model
│   └── ITaskRepository.cs    # Repository contract
├── Application/              # Business logic and use cases
│   ├── DTOs/                 # Data Transfer Objects
│   └── Services/             # Business services
├── Infrastructure/           # External concerns (DB, Auth)
│   ├── Data/                 # EF Core DbContext
│   ├── Repositories/         # Repository implementations
│   └── AuthService.cs        # Authentication logic
└── Controllers/              # API endpoints
```

## Features

- ✅ Create, update, and view tasks
- ✅ Role-based access control (Admin/User)
- ✅ JWT-based authentication
- ✅ In-memory database (EF Core)
- ✅ SOLID principles applied
- ✅ Unit tests included

## Prerequisites

- .NET 10.0 SDK or later
- Any IDE (Visual Studio, VS Code, Rider)

## Setup Instructions

### 1. Clone/Navigate to the Project

```bash
cd "c:\Users\Lenovo\Projects\Task Managment\task managment"
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the Application

```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or the port shown in console).

### 4. Run Tests

```bash
cd TaskManagement.Tests
dotnet test
```

## Test Users

The system has two hardcoded users for testing:

| Username | Password  | Role  | User ID |
|----------|-----------|-------|---------|
| admin    | admin123  | Admin | 1       |
| user     | user123   | User  | 2       |

## API Documentation

### Authentication

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

Use this token in subsequent requests:
```http
Authorization: Bearer {token}
```

### Task Endpoints

#### 1. Create Task
```http
POST /api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Complete project documentation",
  "description": "Write comprehensive README",
  "dueDate": "2024-12-31T23:59:59Z"
}
```

**Response:** `201 Created`
```json
{
  "id": 1,
  "title": "Complete project documentation",
  "description": "Write comprehensive README",
  "isCompleted": false,
  "createdAt": "2024-03-08T10:00:00Z",
  "dueDate": "2024-12-31T23:59:59Z",
  "ownerUserId": "2"
}
```

#### 2. Update Task
```http
PUT /api/tasks/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Updated title",
  "description": "Updated description",
  "dueDate": "2024-12-31T23:59:59Z"
}
```

**Response:** `200 OK` (returns updated task)

**Notes:**
- Users can only update their own tasks
- Returns `403 Forbidden` if trying to update another user's task
- Returns `404 Not Found` if task doesn't exist

#### 3. Get Tasks
```http
GET /api/tasks
Authorization: Bearer {token}
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "title": "Task 1",
    "description": "Description",
    "isCompleted": false,
    "createdAt": "2024-03-08T10:00:00Z",
    "dueDate": "2024-12-31T23:59:59Z",
    "ownerUserId": "2"
  }
]
```

**Behavior:**
- **Regular users**: See only their own tasks
- **Admin users**: See all tasks from all users

#### 4. Mark Task as Completed (Admin Only)
```http
PATCH /api/tasks/{id}/complete
Authorization: Bearer {token}
```

**Response:** `200 OK` (returns updated task with `isCompleted: true`)

**Notes:**
- Only users with `Admin` role can access this endpoint
- Returns `403 Forbidden` for non-admin users
- Returns `404 Not Found` if task doesn't exist

## Design Principles

### SOLID Principles Applied

1. **Single Responsibility**: Each class has one reason to change
   - `TaskService` handles business logic
   - `TaskRepository` handles data access
   - Controllers handle HTTP concerns

2. **Open/Closed**: Extensible through interfaces
   - `ITaskRepository` allows different implementations
   - `ITaskService` can be extended without modifying existing code

3. **Liskov Substitution**: Interfaces can be substituted
   - Any `ITaskRepository` implementation works with `TaskService`

4. **Interface Segregation**: Focused interfaces
   - `ITaskRepository` contains only necessary methods
   - `ITaskService` exposes only required operations

5. **Dependency Inversion**: Depend on abstractions
   - Services depend on `ITaskRepository`, not concrete implementation
   - Controllers depend on `ITaskService`, not concrete service

### Clean Architecture Benefits

- **Testability**: Business logic isolated from infrastructure
- **Maintainability**: Clear separation of concerns
- **Flexibility**: Easy to swap implementations (e.g., switch from in-memory to SQL database)
- **Independence**: Domain layer has no external dependencies

## Testing

The project includes unit tests for the `TaskService` class:

- ✅ Task creation with correct properties
- ✅ Authorization checks for task updates
- ✅ Mark as completed functionality

Run tests with:
```bash
dotnet test
```

## Example Usage Flow

### 1. Login as Regular User
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"user","password":"user123"}'
```

### 2. Create a Task
```bash
curl -X POST https://localhost:5001/api/tasks \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{"title":"My Task","description":"Task details","dueDate":"2024-12-31T23:59:59Z"}'
```

### 3. View Your Tasks
```bash
curl -X GET https://localhost:5001/api/tasks \
  -H "Authorization: Bearer {token}"
```

### 4. Login as Admin
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'
```

### 5. Mark Task as Completed (Admin Only)
```bash
curl -X PATCH https://localhost:5001/api/tasks/1/complete \
  -H "Authorization: Bearer {admin_token}"
```

## Security Considerations

- JWT tokens expire after 2 hours
- Passwords are hardcoded for demo purposes (use proper authentication in production)
- Role-based authorization enforced at controller level
- Ownership validation in service layer

## Future Enhancements

- Add pagination for task lists
- Implement task filtering and sorting
- Add task categories/tags
- Implement proper user management
- Add integration tests
- Switch to persistent database (SQL Server, PostgreSQL)
- Add logging and monitoring
- Implement refresh tokens

## License

This is a demonstration project for educational purposes.
