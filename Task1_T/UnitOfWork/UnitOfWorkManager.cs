using Task1_T.Data;
using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;
using Task1_T.Repositories.Employees;
using Task1_T.Repositories.Permissions;
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
            UserPermissions = new BaseRepository<UserPermission>(_dbContext);
            EmployeeDepartments = new BaseRepository<EmployeeDepartment>(_dbContext);

            PermissionRepository =new PermissionRepository(_dbContext);
            EmployeeRepository = new EmployeeRepository(_dbContext);
        }
        public IBaseRepository<Department> Departments { get; }
        public IBaseRepository<Position> Positions { get; }
        public IBaseRepository<Employee> Employees { get; }
        public IBaseRepository<Permission> Permissions { get; }

        public IPermissionRepository PermissionRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public IBaseRepository<UserPermission> UserPermissions { get; }
        public IBaseRepository<EmployeeDepartment> EmployeeDepartments { get; }
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
