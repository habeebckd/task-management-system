using task_managment.Application.DTOs;

namespace task_managment.Application.Services;

public interface ITaskService
{
    Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId);
    Task<TaskResponseDto> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId);
    Task<IEnumerable<TaskResponseDto>> GetUserTasksAsync(string userId);
    Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
    Task<TaskResponseDto> MarkAsCompletedAsync(int id);
}
