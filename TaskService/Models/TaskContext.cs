using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace TaskService.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
        public DbSet<Task> tasks { get; set; }

        
    }
}
