using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Routes;
using Task1_T.Services.Employees;
using Task1_T.Services.Manages;

namespace Task1_T.Controllers
{
    public class ManagingController : BaseController
    {
        private readonly IManagerService _manageService;
        public ManagingController(IManagerService manageService)
        {
            _manageService = manageService;
        }

        [HttpGet(ApiRoutes.Manage.GetDependentEmployees)]
        //[ClaimRequirementFilter(PermissionNames.Manage.GetDependentEmployees)]
        public async Task<IActionResult> GetDependentEmployees(int employeeId)
        {
            return Ok(await _manageService.GetDependentEmployeesAsync(employeeId));
        }


        [HttpGet(ApiRoutes.Manage.GetManagerEmployees)]
        //[ClaimRequirementFilter(PermissionNames.Manage.GetManagerEmployees)]
        public async Task<IActionResult> GetManagerEmployees(int employeeId)
        {
            return Ok(await _manageService.GetManagerEmployeesAsync(employeeId));
        }
    }
}
