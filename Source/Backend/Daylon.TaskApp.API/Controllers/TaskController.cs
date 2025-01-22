using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.TaskApp.API.Controllers
{
    public class TaskController : TaskAppBaseController
    {
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
