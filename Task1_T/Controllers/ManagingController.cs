using Microsoft.AspNetCore.Mvc;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Routes;
using Task1_T.Services.Employees;

namespace Task1_T.Controllers
{
    public class ManagingController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public ManagingController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet(ApiRoutes.Manage.GetAll)]
        public async Task<IActionResult> GetAll(int employeeId)
        {
            return Ok(await _employeeService.GetEmployeeByIdAsync(employeeId));
        }


        [HttpGet(ApiRoutes.Manage.Get)]
        public async Task<IActionResult> Get(int employeeId)
        {
            return Ok(await _employeeService.GetEmployeeByIdAsync(employeeId));
        }
    }
}
