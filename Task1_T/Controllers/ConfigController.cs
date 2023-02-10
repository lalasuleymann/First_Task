using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Task1_T.Constants;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Routes;
using Task1_T.Services.Departments;
using Task1_T.Services.Employees;
using Task1_T.Services.Permissions;
using Task1_T.Services.Positions;
using Task1_T.Services.UserPermissions;
using Task1_T.Services.Users;

namespace Task1_T.Controllers
{
    public class ConfigController :BaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IHttpContextAccessor context;

        public ConfigController(IPermissionService permissionService, IUserService userService,
            IUserPermissionService userPermissionService, IHttpContextAccessor httpContext)
        {
            _permissionService = permissionService;
            _userService = userService;
            _userPermissionService = userPermissionService;
            context = httpContext;
        }

        [HttpGet(ApiRoutes.Config.GetAll)]
        public async Task<IActionResult> Get()
        {
            var currentUser = context.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            var allPermissions = await _permissionService.GetPermissionsAsync();
            List<GetUserPermissionOutput>? grantedPermissions = null;

            if (currentUser is not null)
            {
                grantedPermissions = await _userPermissionService.GetUserPermissionsWithEmailAsync(currentUser);
            }
            var config = new
            {
                permissions = new
                {
                    allPermissions,
                    grantedPermissions
                },
                currentUser
            };
            return Ok(config);
        }
    }
}
