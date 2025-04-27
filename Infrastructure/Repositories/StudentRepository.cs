using Applications.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync() => await _context.Students.ToListAsync();

        public async Task<int> AddStudentAsync(Student student)
        {
            if(student == null)
                throw new ArgumentNullException(nameof(student));

            await _context.Students.AddAsync(student);
            student.Id = await _context.SaveChangesAsync();
        
            return student.Id;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            _context.Students.Remove(student);           
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteByNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            var student = await _context.Students.FirstOrDefaultAsync(n => n.LName == lastName);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }
                
        public async Task<Student?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be positive", nameof(id));

            return await _context.Students.FindAsync(id);
        }
                
        public async Task<Student?> GetByNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            return await _context.Students.FirstOrDefaultAsync(n => n.LName == lastName);
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            _context.Students.Update(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DoesStudentExistAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            return await _context.Students.AnyAsync(n => n.LName == lastName);
        }
    
        public async Task<bool> DoesStudentExistAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be positive", nameof(id));

            return await _context.Students.AnyAsync(x => x.Id == id);
        }
    }
}
