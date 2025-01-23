using earPass.Domain.Repositories;
using earPass.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace earPass.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly EarPassDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(EarPassDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IQueryable<T>> GetAsync()
    {
        return _dbSet.AsNoTracking().AsQueryable();

    }
    public async Task<T> GetByIdAsync(string id)
    {
       return await _dbSet.FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
