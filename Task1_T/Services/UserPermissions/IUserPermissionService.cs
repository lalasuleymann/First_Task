using Task1_T.Models.Dtos.UserPermissions;

namespace Task1_T.Services.UserPermissions
{
    public interface IUserPermissionService
    {
        Task CreateUserPermissionsAsync(int userId, SaveUserPermissionRequest request);
        Task<List<GetUserPermissionOutput>> GetAllUserPermissionsAsync(int id);
        Task<List<GetUserPermissionOutput>> GetUserPermissionsWithEmailAsync(string email);
        Task<bool> CheckUserPermissions(CheckUserPermissions request);
        Task DeleteUserOldPermissions(int userId);
    }
}
