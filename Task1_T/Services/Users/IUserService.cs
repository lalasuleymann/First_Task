using Microsoft.Extensions.Caching.Memory;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Models.Permissions;

namespace Task1_T.Services.Users
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(string name, string surname, string email, string password, string repassword);
        Task<AuthResponse> LoginAsync(string email, string password);
        Task<GetAllUserResponses> GetSignedUsers();
        Task<ICollection<PermissionDto>> CacheUserPermissions(int userId);
    }
}
