using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task1_T.Data;
using Task1_T.Models.Shared;

namespace Task1_T.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : CommonEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> DbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            var query = DbSet.Where(expression).AsQueryable();
            return query;
        }

        public IQueryable<T> GetQuery(params string[]? includes)
        {
            var query = DbSet.Where(x => !x.IsDeleted).AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
        public async Task<List<T>> GetAllAsync(params string[]? includes)
        {
            return await GetQuery(includes).IgnoreAutoIncludes().ToListAsync();
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetQuery().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            var addedItem = (await DbSet.AddAsync(entity)).Entity;
            return addedItem;
        }

        public async Task AddMultipleAsync(List<T> entities)
        {
           await DbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (item != null)
            {
                DbSet.Update(item);
                item.ModifiedDate = DateTime.UtcNow;
            }
        }
        public async Task DeleteAsync(T entity)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (item != null)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.UtcNow;
            }
        }
    }
}
