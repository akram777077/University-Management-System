using Applications.DTOs;
using Applications.Interfaces.Repositories;
using Applications.Interfaces.Services;
using Applications.Shared;
using Applications.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Applications.Services
{
    public class CourseService(ICourseRepo repository, IMapper mapper, IMyLogger logger) : ICourseService
    {
        public async Task<Result<IReadOnlyCollection<CourseDto>>> GetAllAsync()
        {
            try
            {
                var courses = await repository.GetAllAsync();
                if (!courses.Any())
                {
                    return Result<IReadOnlyCollection<CourseDto>>.Failure(
                        "No courses found in the system", ErrorType.NotFound);
                }
                var coursesDto = mapper.Map<IReadOnlyCollection<CourseDto>>(courses);
                return Result<IReadOnlyCollection<CourseDto>>.Success(coursesDto);
            }
            catch (Exception ex)
            {
                logger.LogError("Database error retrieving all courses", ex);
                return Result<IReadOnlyCollection<CourseDto>>
                    .Failure("Failed to retrieve courses due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result<int>> AddAsync(CourseDto courseDto)
        {
            var course = mapper.Map<Course>(courseDto);
            try
            {
                await repository.AddAsync(course);
                return Result<int>.Success(course.Id);
            }
            catch (Exception ex)
            {
                logger.LogError("Database error adding new course", ex, new { courseDto });
                return Result<int>.Failure("Failed to create course due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result> UpdateAsync(CourseDto courseDto)
        {
            var course = mapper.Map<Course>(courseDto);
            try
            {
                await repository.UpdateAsync(course);
                return Result.Success;
            }
            catch (Exception ex)
            {
                logger.LogError("Database error updating course", ex, new { courseDto });
                return Result.Failure("Failed to update course due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0)
                return Result.Failure("Invalid course ID provided", ErrorType.BadRequest);

            try
            {
                await repository.DeleteAsync(id);
                return Result.Success;
            }
            catch (Exception ex)
            {
                logger.LogError("Database error deleting course", ex, new { id });
                return Result.Failure("Failed to delete course due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result<CourseDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return Result<CourseDto>.Failure("Invalid course ID provided", ErrorType.BadRequest);

            try
            {
                var course = await repository.GetByIdAsync(id);
                if (course == null)
                    return Result<CourseDto>.Failure("Course not found with the specified ID", ErrorType.NotFound);

                var courseDto = mapper.Map<CourseDto>(course);
                return Result<CourseDto>.Success(courseDto);
            }
            catch (Exception ex)
            {
                logger.LogError("Database error retrieving course", ex, new { id });
                return Result<CourseDto>.Failure("Failed to retrieve course due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result<CourseDto>> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result<CourseDto>.Failure("Course name is required", ErrorType.BadRequest);

            try
            {
                var course = await repository.GetByNameAsync(name);
                if (course == null)
                {
                    return Result<CourseDto>.Failure(
                        "Course not found with the specified name", ErrorType.NotFound);
                }
                var courseDto = mapper.Map<CourseDto>(course);
                return Result<CourseDto>.Success(courseDto);
            }
            catch (Exception ex)
            {
                logger.LogError("Database error retrieving course", ex, new { name });
                return Result<CourseDto>.Failure("Failed to retrieve course due to a system error", ErrorType.InternalServerError);
            }
        }

        public async Task<Result<IReadOnlyCollection<CourseDto>>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                return Result<IReadOnlyCollection<CourseDto>>.Failure("Invalid price range provided", ErrorType.BadRequest);

            try
            {
                var courses = await repository.GetByPriceRangeAsync(minPrice, maxPrice);
                if (!courses.Any())
                {
                    return Result<IReadOnlyCollection<CourseDto>>.Failure(
                        "No courses found in the specified price range", ErrorType.NotFound);
                }
                var coursesDto = mapper.Map<IReadOnlyCollection<CourseDto>>(courses);
                return Result<IReadOnlyCollection<CourseDto>>.Success(coursesDto);
            }
            catch (Exception ex)
            {
                logger.LogError("Database error retrieving courses by price range", ex, new { minPrice, maxPrice });
                return Result<IReadOnlyCollection<CourseDto>>.Failure("Failed to retrieve courses due to a system error", ErrorType.InternalServerError);
            }
        }
    }
}

