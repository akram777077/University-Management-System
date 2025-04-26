using Applications.DTOs;
using Domain.Entities;

namespace Applications.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByNameAsync(string name);
        Task<int> AddStudent(Student student);
        Task<Student> UpdateAsync(int id, Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
