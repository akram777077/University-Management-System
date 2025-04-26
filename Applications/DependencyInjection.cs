using Applications.Interfaces.Repositories;
using Applications.Mappers;
using Applications.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
