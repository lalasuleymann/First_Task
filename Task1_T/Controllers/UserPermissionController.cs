using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Routes;
using Task1_T.Services.Employees;
using Task1_T.Services.UserPermissions;

namespace Task1_T.Controllers
{
    public class UserPermissionController : BaseController
    {
        private readonly IUserPermissionService _userPermissionService;
        public UserPermissionController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        [HttpGet(ApiRoutes.UserPermission.GetAll)]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await _userPermissionService.GetAllUserPermissionsAsync(id));
        }


        [HttpPost(ApiRoutes.UserPermission.Check)]
        public async Task<bool> CheckUserPermissions([FromBody] CheckUserPermissions request)
        {
            return (await _userPermissionService.CheckUserPermissions(request));
        }


        [HttpPost(ApiRoutes.UserPermission.Create)]
        public async Task<IActionResult> Create(int userId, SaveUserPermissionRequest request)
        {
            await _userPermissionService.CreateUserPermissionsAsync(userId, request);
            return CreatedAtAction(nameof(GetAll), new { id = userId});
        }


        [HttpDelete(ApiRoutes.UserPermission.Delete)]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userPermissionService.DeleteUserOldPermissions(userId);
            return NoContent();
        }
    }
}
