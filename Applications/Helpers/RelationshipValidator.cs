using Applications.DTOs.Enrollment;
using Applications.Interfaces.Repositories;
using Applications.Interfaces.UnitOfWorks;
using Applications.Shared;
using Domain.Enums;
using static Applications.Shared.Result;

namespace Applications.Helpers;

public static class EnrollmentValidator
{
    public static async Task<Result> ValidateForEnrollmentAsync(this EnrollmentRequest request, IUnitOfWork uow)
    {
        var requirementsValidation = await ValidatePrerequisitesAsync(request, uow);
        
        if (!requirementsValidation.IsSuccess)
            return requirementsValidation;
            
        var canStudentEnroll = await uow.Enrollments.CanEnrollInProgramAsync(
            request.StudentId.Value, request.ProgramId.Value);
            
        if (!canStudentEnroll)
        {
            return Failure(
                "Student cannot enroll: already has an active, on-leave, or completed enrollment in this program", 
                ErrorType.Conflict);
        }
        
        return Success;
    }

    public static async Task<Result> ValidatePrerequisitesAsync(this EnrollmentRequest request, IUnitOfWork uow)
    {
        // Validate required fields
        if (!request.StudentId.HasValue || !request.ProgramId.HasValue || !request.ServiceApplicationId.HasValue)
            return Failure("Student ID, Program ID, and Service Application ID are required", ErrorType.BadRequest);
        
        // Validate entities exist
        var studentExists = await uow.Students.DoesExistsAsync(request.StudentId.Value);
        if (!studentExists)
            return Failure("Student not found", ErrorType.NotFound);

        var programExists = await uow.Programs.DoesExistsAsync(request.ProgramId.Value);
        if (!programExists)
            return Failure("Program not found", ErrorType.NotFound);

        var serviceApplication = await uow.ServiceApplications.GetByIdAsync(request.ServiceApplicationId.Value);
        if (serviceApplication == null)
            return Failure("Service application not found", ErrorType.NotFound);

        // Validate service application status
        if (serviceApplication.Status != ApplicationStatus.Completed)
            return Failure("Service application must be completed before enrollment", ErrorType.BadRequest);
        
        return Success;
    }
}