using Applications.DTOs;
using Applications.Shared;

namespace Applications.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Result<int>> AddAsync(StudentDto studentDto);
        Task<Result> DeleteAsync(int id);
        Task<Result> DeleteAsync(string lastName);
        Task<Result> DoesExistAsync(int id);
        Task<Result> DoesExistAsync(string lastName);
        Task<Result<IReadOnlyCollection<StudentDto>>> GetAllAsync();
        Task<Result<StudentDto>> GetByIdAsync(int id);
        Task<Result<StudentDto>> GetByNameAsync(string lastName);
        Task<Result> UpdateAsync(StudentDto studentDto);
    }
}
