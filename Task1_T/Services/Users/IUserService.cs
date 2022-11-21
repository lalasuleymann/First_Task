using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;

namespace Task1_T.Services.Users
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
