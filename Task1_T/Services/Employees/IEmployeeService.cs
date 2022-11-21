using Task1_T.Models.Dtos.Employees;

namespace Task1_T.Services.Employees
{
    public interface IEmployeeService
    {
        Task<EmployeeGetAllResponse> GetEmployeesAsync();
        Task<EmployeeGetResponse> GetEmployeeByIdAsync(int employeeId);
        Task<EmployeeGetResponse> CreateEmployeeAsync(SaveEmployeeRequest request);
        Task UpdateEmployeeAsync(int employeeId, SaveEmployeeRequest request);
        Task DeleteEmployeeAsync(int employeeId);
    }
}
