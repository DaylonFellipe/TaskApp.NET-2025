using AutoMapper;
using Daylon.TaskApp.Communication.Requests;
using Daylon.TaskApp.Domain.Entities;

namespace Daylon.TaskApp.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterTaskJson, Domain.Entities.Task>();
        }
    }
}
