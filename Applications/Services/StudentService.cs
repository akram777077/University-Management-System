using Applications.DTOs;
using Applications.Interfaces.Repositories;
using Applications.Interfaces.Services;
using Applications.Shared;
using Applications.Utilities;
using AutoMapper;
using Domain.Entities;

namespace Applications.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMyLogger _logger;

        public StudentService(IStudentRepository repository, IMapper mapper, IMyLogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyCollection<StudentDto>>> GetAllAsync()
        {
            try
            {
                //Refactor later to use AutoMapper Projection when the database grows
                var students = await _repository.GetAllAsync();
                if (students == null || students.Count == 0)
                    return Result<IReadOnlyCollection<StudentDto>>.Failure("No students found in the system");

                var studentsDto = _mapper.Map<IReadOnlyCollection<StudentDto>>(students);
                return Result<IReadOnlyCollection<StudentDto>>.Success(studentsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error retrieving all student", ex);
                return Result<IReadOnlyCollection<StudentDto>>
                    .Failure("Failed to retrieve students due to a system error");
            }
        }

        public async Task<Result<int>> AddAsync(StudentDto studentDto)
        {
            if (studentDto == null)
                return Result<int>.Failure("Student information is required");

            var student = _mapper.Map<Student>(studentDto);

            try
            {
                int id = await _repository.AddAsync(student);

                if (id > 0)
                    return Result<int>.Success(id);

                return Result<int>.Failure("Failed to create new student record");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error adding new student", ex, new { studentDto });
                return Result<int>.Failure("Failed to create student due to a system error");
            }
        }

        public async Task<Result> UpdateAsync(StudentDto studentDto)
        {
            if (studentDto == null)
                return Result.Failure("Student information is required for update");

            var student = _mapper.Map<Student>(studentDto);
            try
            {
                bool isUpdated = await _repository.UpdateAsync(student);

                if (isUpdated)
                    return Result.Success;

                return Result.Failure($"Failed to update student");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error updating student", ex, new { studentDto });
                return Result.Failure("Failed to update student due to a system error");
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0)
                return Result.Failure("Invalid student ID provided");

            try
            {
                bool isDeleted = await _repository.DeleteAsync(id);

                if (isDeleted)
                    return Result.Success;

                return Result.Failure("Student not found");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error deleting student", ex, new { id });
                return Result.Failure("Failed to delete student due to a system error");
            }

        }

        public async Task<Result> DeleteAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure("Last name is required");

            try
            {
                bool isDeleted = await _repository.DeleteAsync(lastName);

                if (isDeleted)
                    return Result.Success;

                return Result.Failure("Student not found");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error deleting student", ex, new { lastName });
                return Result.Failure("Failed to delete student due to a system error");
            }
        }

        public async Task<Result> DoesExistAsync(int id)
        {
            if (id <= 0)
                return Result.Failure("Invalid student ID provided");

            try
            {
                var isFound = await _repository.DoesExistAsync(id);

                if (isFound)
                    return Result.Success;

                return Result.Failure("Student not found with the specified ID");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error checking student existence", ex, new { id });
                return Result.Failure("Failed to verify student existence due to a system error");
            }
        }

        public async Task<Result> DoesExistAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure("Last name is required");

            try
            {
                var isFound = await _repository.DoesExistAsync(lastName);

                if (isFound)
                    return Result.Success;

                return Result.Failure("Student not found with the specified last name");
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error checking student existence", ex, new { lastName });
                return Result.Failure("Failed to verify student existence due to a system error");
            }
        }

        public async Task<Result<StudentDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return Result<StudentDto>.Failure("Invalid student ID provided");

            try
            {
                var student = await _repository.GetByIdAsync(id);
                if (student == null)
                    return Result<StudentDto>.Failure("Student not found with the specified ID");

                var studentDto = _mapper.Map<StudentDto>(student);
                return Result<StudentDto>.Success(studentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error retrieving student", ex, new { id });
                return Result<StudentDto>.Failure("Failed to retrieve student due to a system error");
            }
        }

        public async Task<Result<StudentDto>> GetByNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return Result<StudentDto>.Failure("Last name is required");

            try
            {
                var student = await _repository.GetByNameAsync(lastName);
                if (student == null)
                    return Result<StudentDto>.Failure("Student not found with the specified last name");

                var studentDto = _mapper.Map<StudentDto>(student);
                return Result<StudentDto>.Success(studentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Database error retrieving student", ex, new { lastName });
                return Result<StudentDto>.Failure("Failed to retrieve student due to a system error");
            }
        }

    }
}

