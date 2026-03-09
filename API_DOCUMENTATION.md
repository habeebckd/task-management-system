# API Documentation

## Base URL
```
https://localhost:5001
```

## Authentication

All endpoints except `/api/auth/login` require JWT Bearer token authentication.

### Login Endpoint

**POST** `/api/auth/login`

Authenticates a user and returns a JWT token.

**Request Body:**
```json
{
  "username": "string",
  "password": "string"
}
```

**Test Credentials:**
- Admin: `username: "admin"`, `password: "admin123"`
- User: `username: "user"`, `password: "user123"`

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response (401 Unauthorized):**
```json
{
  "message": "Invalid credentials"
}
```

---

## Task Endpoints

### Create Task

**POST** `/api/tasks`

Creates a new task for the authenticated user.

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "title": "string (required, max 200 chars)",
  "description": "string (optional, max 1000 chars)",
  "dueDate": "2024-12-31T23:59:59Z (optional)"
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "title": "Complete project",
  "description": "Finish the task management system",
  "isCompleted": false,
  "createdAt": "2024-03-08T10:00:00Z",
  "dueDate": "2024-12-31T23:59:59Z",
  "ownerUserId": "2"
}
```

---

### Update Task

**PUT** `/api/tasks/{id}`

Updates an existing task. Users can only update their own tasks.

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Path Parameters:**
- `id` (integer): Task ID

**Request Body:**
```json
{
  "title": "string (required, max 200 chars)",
  "description": "string (optional, max 1000 chars)",
  "dueDate": "2024-12-31T23:59:59Z (optional)"
}
```

**Response (200 OK):**
Returns the updated task object.

**Response (403 Forbidden):**
User attempted to update another user's task.

**Response (404 Not Found):**
Task with specified ID doesn't exist.

---

### Get Tasks

**GET** `/api/tasks`

Retrieves tasks based on user role.

**Headers:**
```
Authorization: Bearer {token}
```

**Behavior:**
- **Regular Users**: Returns only tasks owned by the authenticated user
- **Admin Users**: Returns all tasks from all users

**Response (200 OK):**
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
  },
  {
    "id": 2,
    "title": "Task 2",
    "description": "Another task",
    "isCompleted": true,
    "createdAt": "2024-03-07T09:00:00Z",
    "dueDate": null,
    "ownerUserId": "1"
  }
]
```

---

### Mark Task as Completed

**PATCH** `/api/tasks/{id}/complete`

Marks a task as completed. **Admin role required.**

**Headers:**
```
Authorization: Bearer {token}
```

**Path Parameters:**
- `id` (integer): Task ID

**Response (200 OK):**
Returns the updated task with `isCompleted: true`.

**Response (403 Forbidden):**
User doesn't have Admin role.

**Response (404 Not Found):**
Task with specified ID doesn't exist.

---

## Error Responses

### 401 Unauthorized
```json
{
  "message": "Unauthorized"
}
```
Occurs when:
- No token provided
- Invalid token
- Expired token

### 403 Forbidden
Occurs when:
- User attempts to update another user's task
- Non-admin user attempts to mark task as completed

### 404 Not Found
Occurs when:
- Requested task doesn't exist

### 400 Bad Request
Occurs when:
- Validation fails (e.g., missing required fields, exceeding max length)

---

## Data Models

### TaskEntity (Domain)
```csharp
public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string OwnerUserId { get; set; }
}
```

### CreateTaskDto
```csharp
public class CreateTaskDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
}
```

### UpdateTaskDto
```csharp
public class UpdateTaskDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
}
```

### TaskResponseDto
```csharp
public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string OwnerUserId { get; set; }
}
```

---

## OpenAPI/Swagger

When running in Development mode, OpenAPI documentation is available at:
```
https://localhost:5001/openapi/v1.json
```

You can use tools like Swagger UI or Postman to import this specification.
