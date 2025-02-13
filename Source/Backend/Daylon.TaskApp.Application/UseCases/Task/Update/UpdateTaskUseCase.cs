using AutoMapper;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Communication.Responses;
using Daylon.TaskApp.Domain.Repositories.Task;

namespace Daylon.TaskApp.Application.UseCases.Task.Update
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private readonly ITaskWriteOnlyRepository _writeOnlyRepository;
        private readonly ITaskReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;

        public UpdateTaskUseCase(
            ITaskWriteOnlyRepository writeOnlyRepository,
            ITaskReadOnlyRepository readOnlyRepository,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }


        public async Task<ResponseUpdateTaskJson> ValidateStringsAsync(RequestUpdateTaskJson request)
        {
            var validator = new UpdateStringsTaskValidator();

            var result = await validator.ValidateAsync(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                throw new Exception("Request is not valid");
            }

            var entity = await _readOnlyRepository.GetTaskByIdAsync(request.Id);
            entity.Name = request.Name;
            entity.Description = request.Description;

            await _writeOnlyRepository.UpdateTaskAsync(entity);

            return new ResponseUpdateTaskJson
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };
        }
    }
}
