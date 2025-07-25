﻿using Applications.Interfaces.Auth;
using Applications.Interfaces.Logging;
using Applications.Interfaces.Repositories;
using Applications.Interfaces.Services;
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
            
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IServiceOfferService, ServiceOfferService>();
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IServiceApplicationService, ServiceApplicationService>();
            services.AddScoped<IDocsVerificationService, DocsVerificationService>();
            services.AddScoped<IEntranceExamService, EntranceExamService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IPrerequisiteService, PrerequisiteService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IFinancialHoldService, FinancialHoldService>();
            
            //Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            return services;
        }
    }
}
