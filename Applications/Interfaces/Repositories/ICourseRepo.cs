using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Applications.Interfaces.Repositories
{
    public interface ICourseRepo
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task<Course?> GetByNameAsync(string name);
        Task<IEnumerable<Course>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
