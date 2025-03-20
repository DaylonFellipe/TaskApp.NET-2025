using Daylon.TaskApp.Application.Enum;
using Daylon.TaskApp.Application.Interfaces;
using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Application.UseCases.Task.Update;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.TaskApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService) => _taskService = taskService;

        // GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTask(TaskStatusEnum? status)
        {
            var task = await _taskService.GetTasksAsync(status);

            if (task.Count == 0) return NotFound("No task found");

            return Ok(task);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            return Ok(task);
        }

        // POST

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredTaskJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterTask(
            [FromServices] IRegisterTaskUseCase useCase,
            [FromBody] RequestRegisterTaskJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        // PUT

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTask(
            [FromServices] IUpdateTaskUseCase useCase,
            [FromBody] RequestUpdateTaskJson request)
        {
            var validateStrings = await useCase.ValidateStringsAsync(request);

            return Ok(validateStrings);
        }

        // PATCH

        [HttpPatch("ActiveStatus{id}")]
        [ProducesResponseType(typeof(ResponseUpdateTaskJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeActiveStatusAsync([FromRoute] Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            await _taskService.ChangeActiveStatusAsync(task);

            return Ok(task);
        }

        // DELETE

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            await _taskService.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}

