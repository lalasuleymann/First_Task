using Task1_T.Models.Departments;

namespace Task1_T.Services.Departments
{
    public interface IDepartmentService
    {
        Task<DepartmentGetAllResponse> GetDepartmentsAsync();
        Task<DepartmentGetResponse> GetDepartmentByIdAsync(int departmentId);
        Task<DepartmentGetResponse> CreateDepartmentAsync(SaveDepartmentRequest request);
        Task UpdateDepartmentAsync(int departmentId, SaveDepartmentRequest request);
        Task DeleteDepartmentAsync(int departmentId);
    }
}
