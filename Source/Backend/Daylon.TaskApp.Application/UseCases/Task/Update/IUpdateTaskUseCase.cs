using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;

namespace Daylon.TaskApp.Application.UseCases.Task.Update
{
    public interface IUpdateTaskUseCase
    {
        public Task<ResponseUpdateTaskJson> ValidateStringsAsync(RequestUpdateTaskJson request);
        public Task<ResponseUpdateTaskJson> UpdateName(Guid id, String name);
        public Task<ResponseUpdateTaskJson> UpdateDescription(Guid id, String description);
    }
}
