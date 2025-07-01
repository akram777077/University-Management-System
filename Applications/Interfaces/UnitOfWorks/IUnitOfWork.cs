using Applications.Interfaces.Repositories;

namespace Applications.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ICountryRepository Countries { get; }
    IDocsVerificationRepository DocsVerifications { get; }
    IEnrollmentRepository Enrollments { get; }
    IEntranceExamRepository EntranceExams { get; } 
    IInterviewRepository Interviews { get; }
    IPersonRepository People { get; }
    IProfessorRepository Professors { get; }
    IProgramRepository Programs { get; }
    IServiceApplicationRepository ServiceApplications { get; }
    IServiceOfferRepository ServiceOffers { get; }
    IStudentRepository Students { get; }
    IUserRepository Users { get; }
    ISemesterRepository Semesters { get; }
    ICourseRepository Courses { get; }
    IPrerequisiteRepository Prerequisites { get; }

    Task<int> CompleteAsync();
}