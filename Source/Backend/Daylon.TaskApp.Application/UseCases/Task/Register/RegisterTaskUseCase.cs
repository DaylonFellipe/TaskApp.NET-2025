using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using System.Net.Http.Headers;

namespace Daylon.TaskApp.Application.UseCases.Task.Register
{
    public class RegisterTaskUseCase
    {
        public ResponseRegisteredTaskJson Execute(RequestRegisterTaskJson request)
        {
            //  Validate
            Validate(request);

            //  Map


            //  Save


            return new ResponseRegisteredTaskJson
            {
                Name = request.Name,
            };
        }

        private void Validate(RequestRegisterTaskJson request)
        {
            var validator = new RegisterTaskValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);

                throw new Exception("Request is not valid");
            }
        }
    }
}
