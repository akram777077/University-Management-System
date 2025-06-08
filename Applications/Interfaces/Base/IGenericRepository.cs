using Domain.Interfaces;

namespace Applications.Interfaces.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DoesExistAsync(int id);
    }
}
