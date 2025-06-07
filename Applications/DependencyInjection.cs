using Applications.Interfaces.Repositories;
using Applications.Interfaces.Services;
using Applications.Mappers;
using Applications.Services;
using Applications.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IMyLogger, MyLogger>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
