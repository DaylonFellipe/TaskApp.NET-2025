namespace Daylon.TaskApp.Domain.Repositories.Task
{
    public interface ITaskWriteOnlyRepository
    {
        public System.Threading.Tasks.Task Add(Entities.Task task);
    }
}
