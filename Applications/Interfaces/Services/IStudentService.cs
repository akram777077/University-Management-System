using Applications.DTOs;
using Domain.Entities;

namespace Applications.Interfaces.Repositories
{
    public interface IStudentService
    {
        Task<IReadOnlyCollection<StudentDto>> GetStudentsAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<StudentDto> GetByNameAsync(string name);
        Task<int> AddStudent(Student student);
        Task<StudentDto> UpdateAsync(int id, StudentDto studentDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
