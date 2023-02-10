using System.Linq.Expressions;
using Task1_T.Models.Dtos.EmployeeDepartment;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;

namespace Task1_T.Repositories.Employees
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        Task<Employee> GetEmployeeForParentId(int parentEmployee);
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto?> GetFirst(int emp);
        Task DeleteEmployee(int empId);
        Task UpdateEmployee(EmployeeDto emp);
        Task<List<EmployeeDto>> GetDependentEmployees(int employeeId);
        Task<List<GetEmployeeDepartmentOutput>> GetDepartmentsByEmployeeId(int id);
    }
}
