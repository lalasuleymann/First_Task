using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;
using Task1_T.Repositories.Employees;
using Task1_T.Repositories.Permissions;

namespace Task1_T.UnitOfWork
{
    public interface IUnitOfWorkService : IDisposable
    {
        IBaseRepository<Department> Departments { get; }
        IBaseRepository<Position> Positions { get; }
        IBaseRepository<Employee> Employees { get; }
        IBaseRepository<Permission> Permissions { get; }
        IBaseRepository<UserPermission> UserPermissions { get; }
        IBaseRepository<EmployeeDepartment> EmployeeDepartments { get; }
        IBaseRepository<User> Users { get; }

         IPermissionRepository PermissionRepository { get; }
         IEmployeeRepository EmployeeRepository { get; }

        int Complete();
    }
}
