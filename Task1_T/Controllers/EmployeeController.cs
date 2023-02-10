using Microsoft.AspNetCore.Mvc;
using Task1_T.Constants;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Routes;
using Task1_T.Services.Employees;
using Task1_T.Services.UserPermissions;
using static Task1_T.Extensions.ClaimRequirementFilter;

namespace Task1_T.Controllers
{
    [Authorize(EmployeePermissions.Employee)]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet(ApiRoutes.Employee.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetEmployeesAsync());
        }

        [HttpGet(ApiRoutes.Employee.Get)]
        public async Task<IActionResult> Get(int employeeId)
        {
            return Ok(await _employeeService.GetEmployeeByIdAsync(employeeId));
        }

        [HttpPost(ApiRoutes.Employee.Create)]
        [Authorize(EmployeePermissions.EmployeeCreate)]
        public async Task<IActionResult> Create(SaveEmployeeRequest request)
        {
            await _employeeService.CreateEmployeeAsync(request);
            return CreatedAtAction(nameof(GetAll), new { request });
        }

        [HttpPut(ApiRoutes.Employee.Update)]
        [Authorize(EmployeePermissions.EmployeeUpdate)]
        public async Task<IActionResult> Update(int employeeId, SaveEmployeeRequest request)
        {
            await _employeeService.UpdateEmployeeAsync(employeeId, request);
            return Ok();
        }

        [HttpDelete(ApiRoutes.Employee.Delete)]
        [Authorize(EmployeePermissions.EmployeeDelete)]
        public async Task<IActionResult> Delete(int employeeId)
        {
                await _employeeService.DeleteEmployeeAsync(employeeId);
                return NoContent();
        }
    }
}
