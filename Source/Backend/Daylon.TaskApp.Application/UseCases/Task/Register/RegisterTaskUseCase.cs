using AutoMapper;
using Daylon.TaskApp.Application.Services.AutoMapper;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Daylon.TaskApp.Domain.Repositories.Task;
using System.Net.Http.Headers;

namespace Daylon.TaskApp.Application.UseCases.Task.Register
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskWriteOnlyRepository _witeOnlyRepository;
        private readonly ITaskReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;

        public RegisterTaskUseCase(
            ITaskWriteOnlyRepository witeOnlyRepository,
            ITaskReadOnlyRepository readOnlyRepository,
            IMapper mapper)
        {
            _witeOnlyRepository = witeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredTaskJson> Execute(RequestRegisterTaskJson request)
        {
            try
            {
                //  Validate
                Validate(request);

                //  Map
                var taskEntity = _mapper.Map<Domain.Entities.Task>(request);

                //  Save
                await _witeOnlyRepository.Add(taskEntity);

                return new ResponseRegisteredTaskJson
                {
                    Name = taskEntity.Name,
                    Description = taskEntity.Description,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||" +
                    e.Message);

                return null;
            }
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
