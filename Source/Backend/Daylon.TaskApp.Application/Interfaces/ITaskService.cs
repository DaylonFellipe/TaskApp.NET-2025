using Daylon.TaskApp.Application.Enum;

namespace Daylon.TaskApp.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<List<Domain.Entities.Task>> GetAllTasksAsync(TaskStatusEnum? status);
    }
}
