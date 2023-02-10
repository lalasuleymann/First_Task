using Task1_T.Models.Dtos.Employees;

namespace Task1_T.Services.Manages
{
    public interface IManagerService
    {
        Task<EmployeeGetResponse> GetManagerEmployeeAsync(int employeeId);
        Task<EmployeeGetAllResponse> GetDependentEmployeesAsync(int employeeId);
    }
}
