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

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTask(TaskStatusEnum? status)
        {
            var task = await _taskService.GetAllTasksAsync(status);

            if (task.Count == 0) return NotFound("No task found");

            return Ok(task);
        }
    }
}

//        [HttpGet("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetAllActivateTask([FromServices] ITaskReadOnlyRepository readOnlyRepository)
//        {
//            var task = await readOnlyRepository.GetAllActiveAsync();

//            if (task.Count == 0) return NotFound("No task found");

//            return Ok(task);
//        }
//    }
//}

//        [HttpGet("ById")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetTaskById(
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, guidId);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await GetTaskAsync(readOnlyRepository, guidId);

//            return Ok(task);
//        }

//        // POST

//        [HttpPost]
//        [ProducesResponseType(typeof(ResponseRegisteredTaskJson), StatusCodes.Status201Created)]
//        public async Task<IActionResult> RegisterTask(
//            [FromServices] IRegisterTaskUseCase useCase,
//            [FromBody] RequestRegisterTaskJson request)
//        {
//            var result = await useCase.Execute(request);

//            return Created(string.Empty, result);
//        }

//        // PUT

//        [HttpPut("NameandDescription")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public async Task<IActionResult> UpdateTask(
//            [FromServices] IUpdateTaskUseCase useCase,
//            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//            [FromBody] RequestUpdateTaskJson request)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, request.Id);
//            if (validateGuid is not OkResult) return validateGuid;

//            var validateStrings = await useCase.ValidateStringsAsync(request);

//            return Ok(validateStrings);
//        }

//        // PATCH

//        [HttpPatch("Name")]
//        [ProducesResponseType(typeof(ResponseUpdateTaskJson), StatusCodes.Status200OK)]
//        public async Task<IActionResult> UpdateNameTask(
//            [FromServices] IUpdateTaskUseCase useCase,
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//            Guid id, String name)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, id);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await useCase.UpdateName(id, name);

//            return Ok(task);
//        }

//        [HttpPatch("Description")]
//        [ProducesResponseType(typeof(ResponseUpdateTaskJson), StatusCodes.Status200OK)]
//        public async Task<IActionResult> UpdateDescriptionTask(
//           [FromServices] IUpdateTaskUseCase useCase,
//           [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//           Guid id, String description)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, id);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await useCase.UpdateDescription(id, description);

//            return Ok(task);
//        }

//        [HttpPatch("InactiveToActive")]
//        [ProducesResponseType(typeof(ResponseUpdateTaskJson), StatusCodes.Status200OK)]
//        public async Task<IActionResult> InactiveToActive(
//            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//            Guid guidId)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, guidId);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await GetTaskAsync(readOnlyRepository, guidId);

//            _ = writeOnlyRepository.InactiveToActiveAsync(task);

//            return Ok(task);
//        }

//        // DELETE

//        [HttpDelete("SoftDeleteOrFinish")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        public async Task<IActionResult> SoftDeleteTask(
//            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//            Guid guidId)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, guidId);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await GetTaskAsync(readOnlyRepository, guidId);

//            _ = writeOnlyRepository.ActiveToInactiveAsync(task);

//            return NoContent();
//        }

//        [HttpDelete("HardDelete")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        public async Task<IActionResult> HardDeleteTask(
//            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
//            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
//            Guid guidId)
//        {
//            var validateGuid = await GetValidateIdAsync(readOnlyRepository, guidId);
//            if (validateGuid is not OkResult) return validateGuid;

//            var task = await writeOnlyRepository.GetTaskByIdToDeleteAsync(guidId);

//            _ = writeOnlyRepository.DeleteTaskAsync(task);

//            return NoContent();
//        }

//        // SUPORT METHODS

//        private async Task<ActionResult> GetValidateIdAsync(
//            ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
//        {
//            if (guidId == Guid.Empty) return BadRequest("Guid is invalid");

//            var task = await readOnlyRepository.GetTaskByIdAsync(guidId);

//            if (task == null) return NotFound("There is no task with this Id");

//            return Ok();
//        }

//        private static async Task<Domain.Entities.Task> GetTaskAsync(
//            ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
//        {
//            var task = await readOnlyRepository.GetTaskByIdAsync(guidId);

//            return task;
//        }
//    }
//}
