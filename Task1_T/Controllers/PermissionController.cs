using Microsoft.AspNetCore.Mvc;
using Task1_T.Models.Permission;
using Task1_T.Routes;
using Task1_T.Services.Permissions;

namespace Task1_T.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService PermissionService)
        {
            _permissionService = PermissionService;
        }

        [HttpGet(ApiRoutes.Permission.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _permissionService.GetPermissionsAsync());
        }


        [HttpGet(ApiRoutes.Permission.Get)]
        public async Task<IActionResult> Get(int PermissionId)
        {
            return Ok(await _permissionService.GetPermissionByIdAsync(PermissionId));
        }


        [HttpPost(ApiRoutes.Permission.Create)]
        public async Task<IActionResult> Create(SavePermissionRequest request)
        {
            return Created(string.Empty, await _permissionService.CreatePermissionAsync(request));
        }


        [HttpPut(ApiRoutes.Permission.Update)]
        public async Task<IActionResult> Update(int PermissionId, SavePermissionRequest request)
        {
            await _permissionService.UpdatePermissionAsync(PermissionId, request);
            return Ok();
        }


        [HttpDelete(ApiRoutes.Permission.Delete)]
        public async Task<IActionResult> Delete(int permissionId)
        {
            await _permissionService.DeletePermissionAsync(permissionId);
            return NoContent();
        }
    }
}
