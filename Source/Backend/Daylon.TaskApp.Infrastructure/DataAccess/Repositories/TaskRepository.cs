using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.EntityFrameworkCore;

namespace Daylon.TaskApp.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskWriteOnlyRepository, ITaskReadOnlyRepository
    {
        private readonly TaskAppDbContext _dbContext;

        public TaskRepository(TaskAppDbContext dbContext) => _dbContext = dbContext;

        // DB

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public async Task AddAsync(Domain.Entities.Task task) => await _dbContext.AddAsync(task);

        public async Task UpdateTaskAsync(Domain.Entities.Task task)
        {
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        // GET

        public async Task<List<Domain.Entities.Task>> GetAllAsync() => await _dbContext.Tasks.AsNoTracking().ToListAsync();

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(Guid Id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == Id);

            return task ?? throw new Exception("There is no task with that id");
        }

        // PATCH

        public async Task ChangeActiveStatusAsync(Domain.Entities.Task task)
        {
            if (task.Active == true)
                task.Active = false;
            else
                task.Active = true;

            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        // DELETE

        public async Task DeleteTaskAsync(Domain.Entities.Task task)
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
