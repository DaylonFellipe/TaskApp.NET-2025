using Microsoft.EntityFrameworkCore;

namespace Daylon.TaskApp.Infrastructure.DataAccess
{
    public class TaskAppDbContext : DbContext
    {
        public TaskAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
