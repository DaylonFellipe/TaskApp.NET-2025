using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;

namespace Daylon.TaskApp.Application.UseCases.Task.Register
{
    public interface IRegisterTaskUseCase
    {
        public Task<ResponseRegisteredTaskJson> Execute(RequestRegisterTaskJson request);
    }
}
