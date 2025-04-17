using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> CreateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, bool tracked = true);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> FindAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, Expression<Func<T, bool>> predicate, bool tracked = true);
    }
}
