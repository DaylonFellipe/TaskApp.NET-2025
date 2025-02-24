using Daylon.TaskApp.Application.DTO.Task;
using Daylon.TaskApp.Application.Enum;
using Daylon.TaskApp.Application.Interfaces;
using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Daylon.TaskApp.Application.Services.Task
{
    public class TaskService : ITaskService
    {
        public readonly ITaskReadOnlyRepository _taskReadOnlyRepository;
        public readonly ITaskWriteOnlyRepository _taskWriteOnlyRepository;
        private readonly IRegisterTaskUseCase _usecase;

        public TaskService(
            ITaskReadOnlyRepository taskReadOnlyRepository,
            ITaskWriteOnlyRepository taskWriteOnlyRepository,
            IRegisterTaskUseCase usecase
            )
        {
            _taskReadOnlyRepository = taskReadOnlyRepository;
            _taskWriteOnlyRepository = taskWriteOnlyRepository;
            _usecase = usecase;
        }

        public async Task<List<Domain.Entities.Task>> GetTasksAsync(TaskStatusEnum? status)
        {
            var taskList = await _taskReadOnlyRepository.GetAllAsync();

            var result = taskList.Where(tasks => status.HasValue
                ? (status.Value == TaskStatusEnum.active ? tasks.Active == true : tasks.Active == false)
                : true).ToList();

            return result;
        }

        public async Task<List<TaskDTO>> GetTasksAsyncDTO()
        {
            var taskList = await _taskReadOnlyRepository.GetAllAsync();

            var result = taskList
                .Where(tasks => tasks.Active == true)
                .Select(task => new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    CreatedOn = task.CreatedOn,
                }).ToList();

            return result;
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskReadOnlyRepository.GetTaskByIdAsync(id);

            return task;
        }

        public async System.Threading.Tasks.Task ChangeActiveStatusAsync(Domain.Entities.Task task)
        {
            await _taskWriteOnlyRepository.ChangeActiveStatusAsync(task);
        }


        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
        {
            var task = await _taskReadOnlyRepository.GetTaskByIdAsync(id);

            await _taskWriteOnlyRepository.DeleteTaskAsync(task);
        }

    }
}
