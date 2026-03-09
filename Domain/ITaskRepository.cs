namespace task_managment.Domain;

public interface ITaskRepository
{
    Task<TaskEntity> CreateAsync(TaskEntity task);
    Task<TaskEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetByOwnerAsync(string ownerUserId);
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<TaskEntity?> UpdateAsync(TaskEntity task);
}
