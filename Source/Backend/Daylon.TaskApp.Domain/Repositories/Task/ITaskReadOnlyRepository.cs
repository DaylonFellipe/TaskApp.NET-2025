namespace Daylon.TaskApp.Domain.Repositories.Task
{
    public interface ITaskReadOnlyRepository
    {
        public Task<List<Domain.Entities.Task>> GetAllAsync();
        public Task<bool> ExistTaskWithIdAsync(Guid id);
        public Task<Domain.Entities.Task> GetTaskByIdAsync(Guid Id);
    }
}
