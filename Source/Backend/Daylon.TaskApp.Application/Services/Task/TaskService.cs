using Daylon.TaskApp.Application.Enum;
using Daylon.TaskApp.Application.Interfaces;
using Daylon.TaskApp.Domain.Repositories.Task;

namespace Daylon.TaskApp.Application.Services.Task
{
    public class TaskService : ITaskService
    {
        public readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        public readonly ITaskWriteOnlyRepository _taskWriteOnlyRepository;

        public TaskService(ITaskReadOnlyRepository taskReadOnlyRepository, ITaskWriteOnlyRepository taskWriteOnlyRepository)
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
            _taskWriteOnlyRepository = taskWriteOnlyRepository;
        }

        public async Task<List<Domain.Entities.Task>> GetAllTasksAsync(TaskStatusEnum? status)
        {
            var taskList = await _taskReadOnlyRepository.GetAllAsync();

            var result = taskList.Where(tasks => status.HasValue
                ? (status.Value == TaskStatusEnum.active ? tasks.Active == true : tasks.Active == false)
                : true).ToList();

            return result;
        }
    }
}
