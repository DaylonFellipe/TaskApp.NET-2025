using Daylon.TaskApp.Application.Interfaces;
using Daylon.TaskApp.Application.Services.AutoMapper;
using Daylon.TaskApp.Application.Services.Task;
using Daylon.TaskApp.Application.UseCases.Task.Register;
using Daylon.TaskApp.Application.UseCases.Task.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Daylon.TaskApp.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddInterfaces(services);
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterTaskUseCase, RegisterTaskUseCase>();
            services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            services.AddScoped(options => autoMapper);
        }

        private static void AddInterfaces(IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
