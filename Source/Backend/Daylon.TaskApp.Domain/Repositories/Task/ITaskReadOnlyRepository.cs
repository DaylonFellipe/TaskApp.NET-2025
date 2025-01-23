namespace Daylon.TaskApp.Domain.Repositories.Task
{
    public interface ITaskReadOnlyRepository
    {
        public Task<List<Domain.Entities.Task>> GetAll();
        public Task<bool> ExistTaskWithId(Guid id);
        public Task<Domain.Entities.Task> GetTaskById(Guid Id);
    }
}
