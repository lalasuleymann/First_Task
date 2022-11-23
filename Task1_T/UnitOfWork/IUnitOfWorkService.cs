using Task1_T.Models.Entities;
using Task1_T.Repositories;

namespace Task1_T.UnitOfWork
{
    public interface IUnitOfWorkService : IDisposable
    {
        IBaseRepository<Department> Departments { get; }
        IBaseRepository<Position> Positions { get; }
        IBaseRepository<Employee> Employees { get; }
        IBaseRepository<Permission> Permissions { get; }
        IBaseRepository<User> Users { get; }
        int Complete();
    }
}
