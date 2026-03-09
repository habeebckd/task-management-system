using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_managment.Application.DTOs;
using task_managment.Application.Services;

namespace task_managment.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var task = await _taskService.CreateTaskAsync(dto, userId);
        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskResponseDto>> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var task = await _taskService.UpdateTaskAsync(id, dto, userId);
            return Ok(task);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
    {
        var isAdmin = User.IsInRole("Admin");
        
        if (isAdmin)
        {
            var allTasks = await _taskService.GetAllTasksAsync();
            return Ok(allTasks);
        }
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userTasks = await _taskService.GetUserTasksAsync(userId);
        return Ok(userTasks);
    }

    [HttpPatch("{id}/complete")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TaskResponseDto>> MarkAsCompleted(int id)
    {
        try
        {
            var task = await _taskService.MarkAsCompletedAsync(id);
            return Ok(task);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
