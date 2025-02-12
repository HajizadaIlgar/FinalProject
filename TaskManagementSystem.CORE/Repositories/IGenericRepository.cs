using TaskManagementSystem.CORE.Entities.Commons;

namespace TaskManagementSystem.CORE.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetWhere(Func<T, bool> expression);
        Task<IEnumerable<T>> GetPaginatedAllAsync(int take, int page);
        Task<bool> IsExistAsync(int id);
        Task AddAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(int id);
        Task<int> SaveAsync();
    }
}
