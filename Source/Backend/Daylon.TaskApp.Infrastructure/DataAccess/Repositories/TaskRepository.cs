using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.EntityFrameworkCore;

namespace Daylon.TaskApp.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskWriteOnlyRepository, ITaskReadOnlyRepository
    {
        private readonly TaskAppDbContext _dbContext;

        public TaskRepository(TaskAppDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Domain.Entities.Task task) => await _dbContext.AddAsync(task);

        public async Task<bool> ExistTaskWithId(Guid id) =>
            await _dbContext.Tasks.AnyAsync(task => task.Id.Equals(id));
    }
}
