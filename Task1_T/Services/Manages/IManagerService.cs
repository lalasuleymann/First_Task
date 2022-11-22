using Task1_T.Models.Dtos.Employees;

namespace Task1_T.Services.Manages
{
    public interface IManagerService
    {
        Task<ICollection<EmployeeDto>> GetEmployeesAsync(int employeeId);
        Task<ICollection<EmployeeDto>> GetEmployeeAsync(int employeeId);
    }
}
