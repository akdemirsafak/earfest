using System.Linq.Expressions;

namespace earPass.Domain.Repositories;

public interface IGenericRepository<T>
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<T> GetByIdAsync(string id);
    Task<IQueryable<T>> GetAsync();
}
