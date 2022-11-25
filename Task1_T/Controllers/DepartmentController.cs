using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Departments;
using Task1_T.PermissionSet;
using Task1_T.Routes;
using Task1_T.Services.Departments;

namespace Task1_T.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet(ApiRoutes.Department.GetAll)]
        [ClaimRequirementFilter(PermissionNames.Department.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetDepartmentsAsync());
        }


        [HttpGet(ApiRoutes.Department.Get)]
        [ClaimRequirementFilter(PermissionNames.Department.Get)]
        public async Task<IActionResult> Get(int departmentId)
        {
            return Ok(await _departmentService.GetDepartmentByIdAsync(departmentId));
        }


        [HttpPost(ApiRoutes.Department.Create)]
        [ClaimRequirementFilter(PermissionNames.Department.Create)]
        public async Task<IActionResult> Create(SaveDepartmentRequest request)
        {
            return Created(string.Empty, await _departmentService.CreateDepartmentAsync(request));
        }


        [HttpPut(ApiRoutes.Department.Update)]
        [ClaimRequirementFilter(PermissionNames.Department.Update)]
        public async Task<IActionResult> Update(int departmentId, SaveDepartmentRequest request)
        {
            await _departmentService.UpdateDepartmentAsync(departmentId, request);
            return Ok();
        }


        [HttpDelete(ApiRoutes.Department.Delete)]
        [ClaimRequirementFilter(PermissionNames.Department.Delete)]
        public async Task<IActionResult> Delete(int departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);
            return NoContent();
        }
    }
}
