using Applications.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public Task<int> AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> UpdateAsync(int id, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
