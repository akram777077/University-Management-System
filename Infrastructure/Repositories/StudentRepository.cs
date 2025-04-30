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

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<int> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            student.Id = await _context.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return false;

            _context.Students.Remove(student);           
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string lastName)
        {
            var student = await _context.Students.FirstOrDefaultAsync(n => n.LName == lastName);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }
                
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }
                
        public async Task<Student?> GetByNameAsync(string lastName)
        {
            return await _context.Students.FirstOrDefaultAsync(n => n.LName == lastName);
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DoesExistAsync(string lastName)
        {
            return await _context.Students.AnyAsync(n => n.LName == lastName);
        }
    
        public async Task<bool> DoesExistAsync(int id)
        {
            return await _context.Students.AnyAsync(x => x.Id == id);
        }
    }
}
