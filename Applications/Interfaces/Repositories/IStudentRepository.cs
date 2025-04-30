using Domain.Entities;

namespace Applications.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByNameAsync(string lastName);
        Task<int> AddAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(string lastName);
        Task<bool> DoesExistAsync(string lastName);
        Task<bool> DoesExistAsync(int id);
    }
}
