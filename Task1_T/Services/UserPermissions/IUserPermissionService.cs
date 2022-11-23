using Task1_T.Models.Dtos.UserPermissions;

namespace Task1_T.Services.UserPermissions
{
    public interface IUserPermissionService
    {
        Task<UserPermissiongetResponse> CreateUserPermissionsAsync(SaveUserPermissionRequest request);
    }
}
