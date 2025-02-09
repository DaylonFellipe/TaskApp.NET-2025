using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.EntityFrameworkCore;

namespace Daylon.TaskApp.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskWriteOnlyRepository, ITaskReadOnlyRepository
    {
        private readonly TaskAppDbContext _dbContext;

        public TaskRepository(TaskAppDbContext dbContext) => _dbContext = dbContext;

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public async Task AddAsync(Domain.Entities.Task task) => await _dbContext.AddAsync(task);

        public async Task<List<Domain.Entities.Task>> GetAllAsync() => await _dbContext.Tasks.AsNoTracking().ToListAsync();

        public async Task<List<Domain.Entities.Task>> GetAllActiveAsync() =>
            await _dbContext.Tasks.AsNoTracking().Where(task => task.Active).ToListAsync();

        public async Task<bool> ExistTaskWithIdAsync(Guid id) =>
            await _dbContext.Tasks.AnyAsync(task => task.Id.Equals(id));

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(Guid Id)
        {
            var task = await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == Id);

            return task!;
        }

        public async Task<Domain.Entities.Task> GetTaskByIdToDeleteAsync(Guid Id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == Id);

            return task!;
        }

        public async Task UpdateTaskAsync(Domain.Entities.Task task)
        {
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ActiveToInactiveAsync(Domain.Entities.Task task)
        {
            task.Active = false;
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }
        public async Task InactiveToActiveAsync(Domain.Entities.Task task)
        {
            task.Active = true;
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Domain.Entities.Task task)
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
