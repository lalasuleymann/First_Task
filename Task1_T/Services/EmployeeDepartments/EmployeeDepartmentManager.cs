using Abp.Domain.Uow;
using Task1_T.Models.Dtos.EmployeeDepartment;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.EmployeeDepartments
{
    public class EmployeeDepartmentManager : IEmployeeDepartmentService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        public EmployeeDepartmentManager(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateEmployeeDepartmentsAsync(int employeeId, SaveEmployeeDepartmentRequest request)
        {
            var employeeDepartmentgetResponse = new EmployeeDepartementGetResponse();

            var employeeDepartments = request.DepartmentIds.Select(departmentId => new Models.Entities.EmployeeDepartment
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId
            }).ToList();

            await _unitOfWork.EmployeeDepartments.AddMultipleAsync(employeeDepartments);

            _unitOfWork.Complete();
        }

        public async Task<List<GetEmployeeDepartmentOutput>> GetAllEmployeeDepartmentsAsync(int id)
        {
            var entities = await _unitOfWork.EmployeeRepository.GetDepartmentsByEmployeeId(id);
            return entities;
        }
    }
}
