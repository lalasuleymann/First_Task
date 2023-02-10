using Task1_T.Models.Permission;
using Task1_T.Models.Permissions;

namespace Task1_T.Services.Permissions
{
    public interface IPermissionService
    {
        Task<PermissionGetAllResponse> GetPermissionsAsync();
        Task<PermissionGetResponse> GetPermissionByIdAsync(int permissionId);
        Task<PermissionGetResponse> CreatePermissionAsync(SavePermissionRequest request);
        Task UpdatePermissionAsync(int permissionId, SavePermissionRequest request);
        Task DeletePermissionAsync(int permissionId);
    }
}
