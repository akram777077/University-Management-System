using Applications.DTOs;
using Applications.Interfaces.Repositories;
using Domain.Entities;

namespace Applications.Services
{
    public class StudentService : IStudentService
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

        public Task<StudentDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<StudentDto>> GetStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> UpdateAsync(int id, StudentDto studentDto)
        {
            throw new NotImplementedException();
        }
    }
}
