using Microsoft.EntityFrameworkCore;
using task_managment.Domain;

namespace task_managment.Infrastructure.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }
}
