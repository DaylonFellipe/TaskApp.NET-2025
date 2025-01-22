using Daylon.TaskApp.Domain.Repositories.Task;
using Daylon.TaskApp.Infrastructure.DataAccess;
using Daylon.TaskApp.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Daylon.TaskApp.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITaskWriteOnlyRepository, TaskRepository>();
            services.AddScoped<ITaskReadOnlyRepository, TaskRepository>();
        }
    }
}
