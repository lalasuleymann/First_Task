using Microsoft.AspNetCore.Mvc;
using Task1_T.Models.Dtos.EmployeeDepartment;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Routes;
using Task1_T.Services.EmployeeDepartments;

namespace Task1_T.Controllers
{
    public class EmployeeDepartmentController : BaseController
    {
        private readonly IEmployeeDepartmentService _employeeDepartmentService;
        public EmployeeDepartmentController(IEmployeeDepartmentService employeeDepartmentService)
        {
            _employeeDepartmentService = employeeDepartmentService;
        }

        [HttpGet(ApiRoutes.EmployeeDepartment.GetAll)]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await _employeeDepartmentService.GetAllEmployeeDepartmentsAsync(id));
        }

        [HttpPost(ApiRoutes.EmployeeDepartment.Create)]
        public async Task<IActionResult> Create(int userId, SaveEmployeeDepartmentRequest request)
        {
            await _employeeDepartmentService.CreateEmployeeDepartmentsAsync(userId, request);
            return CreatedAtAction(nameof(GetAll), new { id = userId });
        }
    }
}
