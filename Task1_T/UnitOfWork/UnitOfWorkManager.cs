using Task1_T.Data;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.Services.Departments;
using Task1_T.Services.Employees;
using Task1_T.Services.Positions;
using Task1_T.Services.Tokens;
using Task1_T.Services.Users;

namespace Task1_T.UnitOfWork
{
    public class UnitOfWorkManager :IUnitOfWorkService
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWorkManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Departments = new BaseRepository<Department>(_dbContext);
            Positions = new BaseRepository<Position>(_dbContext);
            Employees = new BaseRepository<Employee>(_dbContext);
            Users = new BaseRepository<User>(_dbContext);
            Permissions = new BaseRepository<Permission>(_dbContext);
        }
        public IBaseRepository<Department> Departments { get; }
        public IBaseRepository<Position> Positions { get; }
        public IBaseRepository<Employee> Employees { get; }
        public IBaseRepository<Permission> Permissions { get; }
        public IBaseRepository<User> Users { get; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
    }
}
