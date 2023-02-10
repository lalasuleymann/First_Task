using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;

namespace Task1_T.Repositories.Permissions
{
    public interface IPermissionRepository:IBaseRepository<Permission>
    {
        Task DeleteAsync(int id);
        Task<List<GetUserPermissionOutput>> GetPermissionsByUserEmail(string email);
        Task<List<GetUserPermissionOutput>> GetPermissionsByUserId(int id);
    }
}
