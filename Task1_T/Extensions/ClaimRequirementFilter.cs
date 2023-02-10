using Abp.Domain.Repositories;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.Services.Users;
using Task1_T.UnitOfWork;

namespace Task1_T.Extensions
{
    public class ClaimRequirementFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permissions;
        private readonly IUnitOfWorkService _unitOfWork;

        public ClaimRequirementFilter(string permissions, IUnitOfWorkService unitOfWork)
        {
            _permissions = permissions;
            _unitOfWork = unitOfWork;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var email= context.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

            var permissions = _permissions.ToLower().Split(",").ToList();
            bool isAuthorized = await CheckUserPermissions(permissions, email);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private async Task<bool> CheckUserPermissions(List<string> permissions, string email)
        {
            var permissionList = (await _unitOfWork.PermissionRepository.GetPermissionsByUserEmail(email));
          
           var hasPermission= permissionList.Any(permission => permissions.Any(p => permission.Name.ToLower() == p));
            return hasPermission;
        }


        public class AuthorizeAttribute : TypeFilterAttribute
        {
            public AuthorizeAttribute(string permission): base(typeof(ClaimRequirementFilter))
            {
                Arguments = new object[] { permission };
            }
        }
    }
}
