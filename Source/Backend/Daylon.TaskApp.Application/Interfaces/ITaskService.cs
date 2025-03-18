using Daylon.TaskApp.Application.DTO.Task;
using Daylon.TaskApp.Application.Enum;

namespace Daylon.TaskApp.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<List<Domain.Entities.Task>> GetTasksAsync(TaskStatusEnum? status);
        public Task<List<TaskDTO>> GetTasksAsyncDTO();
        public Task<Domain.Entities.Task> GetTaskByIdAsync(Guid Id);
        public Task ChangeActiveStatusAsync(Domain.Entities.Task task);
        public Task DeleteTaskAsync(Guid id);
    }
}
