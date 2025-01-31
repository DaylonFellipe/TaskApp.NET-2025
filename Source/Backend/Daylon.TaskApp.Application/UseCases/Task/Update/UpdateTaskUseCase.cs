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

        public async Task<ResponseUpdateTaskJson> UpdateName(Guid id, String name)
        {
            var validator = new UpdateNameValidator(name);

            var result = await validator.ValidateAsync(name);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                throw new Exception("Request is not valid");
            }

            var entity = await _readOnlyRepository.GetTaskByIdAsync(id);
            entity.Name = name;

            await _writeOnlyRepository.UpdateTaskAsync(entity);

            return new ResponseUpdateTaskJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public async Task<ResponseUpdateTaskJson> UpdateDescription(Guid id, String description)
        {
            var validator = new UpdateDescriptionValidator(description);

            var result = await validator.ValidateAsync(description);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                throw new Exception("Request is not valid");
            }

            var entity = await _readOnlyRepository.GetTaskByIdAsync(id);
            entity.Description = description;

            await _writeOnlyRepository.UpdateTaskAsync(entity);

            return new ResponseUpdateTaskJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
