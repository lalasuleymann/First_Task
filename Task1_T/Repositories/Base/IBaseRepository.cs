using System.Linq.Expressions;
using Task1_T.Models.Shared;

namespace Task1_T.Repositories.Base
{
    public interface IBaseRepository<T> where T : CommonEntity
    {
        Task<List<T>> GetAllAsync(params string[]? includes);
        IQueryable<T> GetQuery(params string[]? includes);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task AddMultipleAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
