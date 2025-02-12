using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSystem.CORE.Entities.Commons;
using TaskManagementSystem.CORE.Repositories;
using TaskManagementSystem.DAL.Contexts;

namespace TaskManagementSystem.DAL.Repositories;

public class GenericRepository<T>(TaskManagementDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table = _context.Set<T>();
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
        //T? entity = await GetByIdAsync(id);
        //Delete(entity!);
    }

    public IQueryable<T> GetAll()
      => Table.AsQueryable();
    public async Task<T?> GetByIdAsync(int id)
            => await Table.FindAsync(id);

    public Task<IEnumerable<T>> GetPaginatedAllAsync(int take, int page)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
            => Table.Where(expression).AsQueryable();

    public IQueryable<T> GetWhere(Func<T, bool> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsExistAsync(int id)
             => await Table.AnyAsync(x => x.Id == id);
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
