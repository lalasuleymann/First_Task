using System.Linq.Expressions;
using Task1_T.Models.Shared;

namespace Task1_T.Repositories
{
    public interface IBaseRepository<T> where T : CommonEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
