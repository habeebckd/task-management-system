using task_managment.Application.DTOs;
using task_managment.Domain;

namespace task_managment.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId)
    {
        var task = new TaskEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false,
            OwnerUserId = userId
        };

        var created = await _repository.CreateAsync(task);
        return MapToDto(created);
    }

    public async Task<TaskResponseDto> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException("Task not found");

        if (task.OwnerUserId != userId)
            throw new UnauthorizedAccessException("You can only update your own tasks");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;

        var updated = await _repository.UpdateAsync(task);
        return MapToDto(updated!);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetUserTasksAsync(string userId)
    {
        var tasks = await _repository.GetByOwnerAsync(userId);
        return tasks.Select(MapToDto);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
    {
        var tasks = await _repository.GetAllAsync();
        return tasks.Select(MapToDto);
    }

    public async Task<TaskResponseDto> MarkAsCompletedAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException("Task not found");

        task.IsCompleted = true;
        var updated = await _repository.UpdateAsync(task);
        return MapToDto(updated!);
    }

    private static TaskResponseDto MapToDto(TaskEntity task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        IsCompleted = task.IsCompleted,
        CreatedAt = task.CreatedAt,
        DueDate = task.DueDate,
        OwnerUserId = task.OwnerUserId
    };
}
