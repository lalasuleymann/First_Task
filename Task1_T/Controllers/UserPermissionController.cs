using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Routes;
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

        [HttpPost(ApiRoutes.UserPermission.Create)]
        [ClaimRequirementFilter(PermissionNames.UserPermission.Create)]
        public async Task<IActionResult> Create(SaveUserPermissionRequest request)
        {
            return Created(string.Empty, await _userPermissionService.CreateUserPermissionsAsync(request));
        }
    }
}
