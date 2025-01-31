namespace Daylon.TaskApp.Domain.Repositories.Task
{
    public interface ITaskWriteOnlyRepository
    {
        public System.Threading.Tasks.Task SaveChangesAsync();
        public System.Threading.Tasks.Task AddAsync(Entities.Task task);
        public System.Threading.Tasks.Task ActiveToInactiveAsync(Entities.Task task);
        public System.Threading.Tasks.Task InactiveToActiveAsync(Entities.Task task);
        public System.Threading.Tasks.Task DeleteTaskAsync(Domain.Entities.Task task);
        public System.Threading.Tasks.Task UpdateTaskAsync(Domain.Entities.Task task);
    }
}
