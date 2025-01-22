﻿using Daylon.TaskApp.Application.Services.AutoMapper;
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
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var task = autoMapper.Map<Domain.Entities.Task>(request);

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
