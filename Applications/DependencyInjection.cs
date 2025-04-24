using Microsoft.Extensions.DependencyInjection;

namespace Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Register Application service here
            //services.AddScoped<ICourseService, CourseService>();

            // Register cross-cutting concerns for the application layer
            //services.AddAutoMapper();

            return services;
        }
    }
}
