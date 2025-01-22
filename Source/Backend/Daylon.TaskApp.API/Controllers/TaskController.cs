using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daylon.TaskApp.API.Controllers
{
    public class TaskController : TaskAppBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredTaskJson), StatusCodes.Status201Created)]
        public IActionResult RegisterTask(RequestRegisterTaskJson request)
        {
            var useCase = new RegisterTaskUseCase();

            var result = useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
