using Task1_T.Models.Dtos.EmployeeDepartment;

namespace Task1_T.Services.EmployeeDepartments
{
    public interface IEmployeeDepartmentService
    {
        Task CreateEmployeeDepartmentsAsync(int employeeId, SaveEmployeeDepartmentRequest request);
        Task<List<GetEmployeeDepartmentOutput>> GetAllEmployeeDepartmentsAsync(int id);
    }
}
