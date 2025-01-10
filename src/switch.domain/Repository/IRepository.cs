using @switch.domain.Entities;

namespace @switch.domain.Repository
{
    public interface IRepository<T> where T : BaseEntity<Guid>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}