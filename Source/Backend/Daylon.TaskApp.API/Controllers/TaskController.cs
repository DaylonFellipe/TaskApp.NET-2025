﻿using Daylon.TaskApp.Application.UseCases.Task.Register;
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
            var task = await readOnlyRepository.GetAllAsync();

            if (task.Count == 0) return NotFound("No task found");

            return Ok(task);
        }

        [HttpGet("By Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskById(
            [FromServices] ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
        {
            var validateGuid = await GetValidatedTaskByIdAsync(readOnlyRepository, guidId);

            if (validateGuid.Result is BadRequestObjectResult) return validateGuid.Result;

            var task = validateGuid.Value;

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

        [HttpDelete("SoftDelete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SoftDeleteTask(
            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
            Guid guidId)
        {
            var validateGuid = await GetValidatedTaskByIdAsync(readOnlyRepository, guidId);

            if (validateGuid.Result is BadRequestObjectResult) return validateGuid.Result;

            var task = validateGuid.Value;

            _ = writeOnlyRepository.ActiveToInactiveAsync(task!);

            return NoContent();
        }

        [HttpDelete("HardDelete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> HardDeleteTask(
            [FromServices] ITaskWriteOnlyRepository writeOnlyRepository,
            [FromServices] ITaskReadOnlyRepository readOnlyRepository,
            Guid guidId)
        {
            var validateGuid = await GetValidatedTaskByIdAsync(readOnlyRepository, guidId);

            if (validateGuid.Result is BadRequestObjectResult) return validateGuid.Result;

            var task = validateGuid.Value;

            _ = writeOnlyRepository.DeleteTaskAsync(task!);

            return NoContent();
        }

        // SUPORT METHODS

        private async Task<ActionResult<Domain.Entities.Task>> GetValidatedTaskByIdAsync(
            ITaskReadOnlyRepository readOnlyRepository, Guid guidId)
        {
            if (guidId == Guid.Empty) return BadRequest("Guid is invalid");

            var task = await readOnlyRepository.GetTaskByIdAsync(guidId);

            if (task == null) return BadRequest("There is no task with this Id");

            return task;
        }
    }
}
