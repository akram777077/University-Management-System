using Applications.DTOs;
using Domain.Entities;

namespace Applications.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByNameAsync(string lastName);
        Task<int> AddStudentAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(Student student);
        Task<bool> DeleteByNameAsync(string lastName);
        Task<bool> DoesStudentExistAsync(string lastName);
        Task<bool> DoesStudentExistAsync(int id);
    }
}
