using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task1_T.Models.Shared;

namespace Task1_T.Repositories
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
        public IQueryable<T> GetQuery()
        {
            return DbSet.Where(x => !x.IsDeleted).AsQueryable();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetQuery().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            var addedItem = (await DbSet.AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();
            return addedItem;
        }
        public async Task UpdateAsync(T entity)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (item != null)
            {
                DbSet.Update(item);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (item != null)
            {
                item.IsDeleted = true;
                item.DeletedDate = DateTime.UtcNow;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
