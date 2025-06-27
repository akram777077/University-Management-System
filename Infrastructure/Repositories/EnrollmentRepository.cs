using Applications.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnrollmentRepository(AppDbContext context) : GenericRepository<Enrollment>(context), IEnrollmentRepository
{
    // Additional methods specific to Program can be added here
    public override async Task<IEnumerable<Enrollment>> GetListAsync()
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Include(x => x.Program)
            .ToListAsync();
    }
    public override async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Include(x => x.Program)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Enrollment?> GetByStudentIdAsync(int studentId)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Include(x => x.Program)
            .FirstOrDefaultAsync(x => x.StudentId == studentId);
    }
}