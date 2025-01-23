using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Daylon.TaskApp.Domain.Repositories.Task;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.TaskApp.API.Controllers
{
    public class TaskController : TaskAppBaseController
    {

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPerson([FromServices] ITaskReadOnlyRepository readOnlyRepository)
        {
            var task = await readOnlyRepository.GetAll();

            if (task.Count == 0) return NotFound("No task found");

            return Ok(task);
        }

        [HttpGet("By Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskById(
            [FromServices] ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
        {
            if (guidId == Guid.Empty) return BadRequest("Guid is required");

            var task = await readOnlyRepository.GetTaskById(guidId);

            if (task == null) return BadRequest("There is no task with this Id");

            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredTaskJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterTask(
            [FromServices] IRegisterTaskUseCase useCase,
            [FromBody] RequestRegisterTaskJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
