namespace Daylon.TaskApp.Domain.Repositories.Task
{
    public interface ITaskReadOnlyRepository
    {
        public Task<bool> ExistTaskWithId(Guid id);
    }
}
