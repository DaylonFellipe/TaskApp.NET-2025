using Microsoft.EntityFrameworkCore;

namespace Daylon.TaskApp.Infrastructure.DataAccess
{
    public class TaskAppDbContext : DbContext
    {
        public TaskAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
    }
}
