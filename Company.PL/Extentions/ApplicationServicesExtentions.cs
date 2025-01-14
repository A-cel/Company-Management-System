using Company.BLL;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Company.PL.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            return services;
        }
    }
}
