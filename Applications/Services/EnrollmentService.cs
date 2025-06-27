using Applications.DTOs.Enrollment;
using Applications.Interfaces.Repositories;
using Applications.Shared;
using Applications.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Applications.Helpers;
using Applications.Interfaces.Services;

namespace Applications.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProgramRepository _programRepository;
    private readonly IServiceApplicationRepository _serviceAppRepository;
    private readonly IMapper _mapper;
    private readonly IMyLogger _logger;

    public EnrollmentService(
        IEnrollmentRepository repository,
        IStudentRepository studentRepository,
        IProgramRepository programRepository,
        IServiceApplicationRepository serviceAppRepository,
        IMapper mapper,
        IMyLogger logger)
    {
        _repository = repository;
        _studentRepository = studentRepository;
        _programRepository = programRepository;
        _serviceAppRepository = serviceAppRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<EnrollmentResponse>>> GetListAsync()
    {
        try
        {
            var enrollments = await _repository.GetListAsync();
            if (!enrollments.Any())
            {
                return Result<IReadOnlyCollection<EnrollmentResponse>>.Failure(
                    "No enrollments found", ErrorType.NotFound);
            }

            var response = _mapper.Map<IReadOnlyCollection<EnrollmentResponse>>(enrollments);
            return Result<IReadOnlyCollection<EnrollmentResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error retrieving enrollments", ex);
            return Result<IReadOnlyCollection<EnrollmentResponse>>.Failure(
                "Failed to retrieve enrollments", ErrorType.InternalServerError);
        }
    }

    public async Task<Result<EnrollmentResponse>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return Result<EnrollmentResponse>.Failure("Invalid enrollment ID", ErrorType.BadRequest);

        try
        {
            var enrollment = await _repository.GetByIdAsync(id);
            if (enrollment == null)
                return Result<EnrollmentResponse>.Failure("Enrollment not found", ErrorType.NotFound);

            var response = _mapper.Map<EnrollmentResponse>(enrollment);
            return Result<EnrollmentResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error retrieving enrollment", ex, new { id });
            return Result<EnrollmentResponse>.Failure(
                "Failed to retrieve enrollment", ErrorType.InternalServerError);
        }
    }

    public async Task<Result<EnrollmentResponse>> GetByStudentIdAsync(int studentId)
    {
        if (studentId <= 0)
            return Result<EnrollmentResponse>.Failure("Invalid student ID", ErrorType.BadRequest);

        try
        {
            var enrollment = await _repository.GetByStudentIdAsync(studentId);
            if (enrollment == null)
                return Result<EnrollmentResponse>.Failure("Enrollment not found for this student", ErrorType.NotFound);

            var response = _mapper.Map<EnrollmentResponse>(enrollment);
            return Result<EnrollmentResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error retrieving enrollment by student", ex, new { studentId });
            return Result<EnrollmentResponse>.Failure(
                "Failed to retrieve enrollment", ErrorType.InternalServerError);
        }
    }

    public async Task<Result<EnrollmentResponse>> AddAsync(EnrollmentRequest request)
    {
        if (request == default)
            return Result<EnrollmentResponse>.Failure("Enrollment data is required", ErrorType.BadRequest);

        var validationResult = await request.ValidateRelationships(
            _studentRepository, _programRepository, _serviceAppRepository);
        
        if (!validationResult.IsSuccess)
            return Result<EnrollmentResponse>.Failure(validationResult.Error, validationResult.ErrorType);

        try
        {
            var enrollment = _mapper.Map<Enrollment>(request);
            enrollment.EnrollmentDate = DateTime.UtcNow;

            int id = await _repository.AddAsync(enrollment);
            if (id <= 0)
                return Result<EnrollmentResponse>.Failure("Failed to create enrollment", ErrorType.BadRequest);

            var response = _mapper.Map<EnrollmentResponse>(enrollment);
            return Result<EnrollmentResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error creating enrollment", ex, new { request });
            return Result<EnrollmentResponse>.Failure("Failed to create enrollment", ErrorType.InternalServerError);
        }
    }

    public async Task<Result> UpdateAsync(int id, EnrollmentRequest request)
    {
        if (request == default)
            return Result<EnrollmentResponse>.Failure("Enrollment data is required", ErrorType.BadRequest);

        try
        {
            var existingEnrollment = await _repository.GetByIdAsync(id);
            if (existingEnrollment == null)
                return Result.Failure("Enrollment not found", ErrorType.NotFound);

            // Validate relationships if they're being updated
            if (request.StudentId.HasValue || request.ProgramId.HasValue || request.ServiceApplicationId.HasValue)
            {
                var validationResult = await request.ValidateRelationships(
                    _studentRepository, _programRepository, _serviceAppRepository);
                
                if (!validationResult.IsSuccess)
                    return Result.Failure(validationResult.Error, validationResult.ErrorType);
            }

            _mapper.Map(request, existingEnrollment);
            existingEnrollment.Id = id;
            bool isUpdated = await _repository.UpdateAsync(existingEnrollment);
            return !isUpdated ? Result.Failure("Failed to update enrollment", ErrorType.BadRequest) : Result.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error updating enrollment", ex, new { id, request });
            return Result.Failure("Failed to update enrollment", ErrorType.InternalServerError);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        if (id <= 0)
            return Result.Failure("Invalid enrollment ID", ErrorType.BadRequest);
        
        try
        {
            bool isDeleted = await _repository.DeleteAsync(id);
            return !isDeleted ? Result.Failure("Enrollment not found", ErrorType.NotFound) : Result.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError("Database error deleting enrollment", ex, new { id });
            return Result.Failure("Failed to delete enrollment", ErrorType.InternalServerError);
        }
    }
}