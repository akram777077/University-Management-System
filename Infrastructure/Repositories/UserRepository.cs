using Applications.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        //User Specific Operations

        public async Task<bool> DeleteAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Username == username);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> DoesExistAsync(int personId)
        {
            return await _context.Students.AnyAsync(x => x.PersonId == personId);
        }
        
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(n => n.Username == username);
        }
    }
}
