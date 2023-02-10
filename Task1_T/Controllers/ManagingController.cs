using Microsoft.AspNetCore.Mvc;
using Task1_T.Constants;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Routes;
using Task1_T.Services.Employees;
using Task1_T.Services.Manages;
using static Task1_T.Extensions.ClaimRequirementFilter;

namespace Task1_T.Controllers
{
    public class ManagingController : BaseController
    {
        private readonly IManagerService _manageService;
        public ManagingController(IManagerService manageService)
        {
            _manageService = manageService;
        }


        [HttpGet(ApiRoutes.Manage.GetManagerEmployee)]
        [Authorize(ManagePermissions.Manager)]
        public async Task<IActionResult> GetManagerEmployee(int employeeId)
        {
            return Ok(await _manageService.GetManagerEmployeeAsync(employeeId));
        }
        
        [HttpGet(ApiRoutes.Manage.GetDependentEmployees)]
        [Authorize(ManagePermissions.Dependent)]
        public async Task<IActionResult> GetDependentEmployees(int employeeId)
        {
            return Ok(await _manageService.GetDependentEmployeesAsync(employeeId));
        }

    }
}
