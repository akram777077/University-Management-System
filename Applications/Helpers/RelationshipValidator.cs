using Applications.DTOs.Enrollment;
using Applications.Interfaces.Repositories;
using Applications.Shared;
using Domain.Enums;
using static Applications.Shared.Result;

namespace Applications.Helpers;

public static class RelationshipValidator
{
    public static async Task<Result> ValidateRelationships(this EnrollmentRequest request, 
        IStudentRepository studentRepository,
        IProgramRepository programRepository,
        IServiceApplicationRepository serviceAppRepository)
    {
        if (!request.StudentId.HasValue || !request.ProgramId.HasValue || !request.ServiceApplicationId.HasValue)
            return Failure("Student, Program and Service Application are required", ErrorType.BadRequest);

        var studentExists = await studentRepository.DoesExistsAsync(request.StudentId.Value);
        if (!studentExists)
            return Failure("Specified student does not exist", ErrorType.NotFound);

        var programExists = await programRepository.DoesExistsAsync(request.ProgramId.Value);
        if (!programExists)
            return Failure("Specified program does not exist", ErrorType.NotFound);

        var serviceAppExists = await serviceAppRepository.DoesExistsAsync(request.ServiceApplicationId.Value);
        if (!serviceAppExists)
            return Failure("Specified service application does not exist", ErrorType.NotFound);

        return Success;
    }
    
    
}