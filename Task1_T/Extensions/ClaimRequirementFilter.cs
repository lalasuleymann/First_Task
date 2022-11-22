using Microsoft.AspNetCore.Mvc.Filters;
using Task1_T.Services.Users;

namespace Task1_T.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ClaimRequirementFilter : Attribute, IAsyncActionFilter
    {
        private readonly string _permission;

        public ClaimRequirementFilter(string permission)
        {
            _permission = permission;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var service = context.HttpContext.RequestServices.GetService<IUserService>();

            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub").Value;

            var permissions  = await service?.CacheUserPermissions(Convert.ToInt32(userId));
            var permissionNames = permissions.Select(x => x.Name);

            if (!permissionNames.Contains(_permission))
            {
                throw new Exception("Your user doesn't include required permission to perform this operation.");
            }

            await next();
        }
    }
}
