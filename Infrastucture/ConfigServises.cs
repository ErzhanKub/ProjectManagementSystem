using Application.Shared;
using Domain.Repositories;
using Infrastructure.DataBase;
using Infrastucture.DataBase;
using Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture
{
    public static class ConfigServises
    {
        public static IServiceCollection AddInfrastruct(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("Default") ??
                    "Server=.; Database=ProjectManagementSystem; Trusted_Connection=SSPI; Encrypt=Optional");
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeRepository, EmployeeRepo>();
            services.AddTransient<IProjectRepository, ProjectRepo>();
            services.AddTransient<ICustomTaskRepository, CustomTaskRepository>();

            return services;
        }
    }
}
