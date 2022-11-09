using Task1_T.Models.Dtos.Users;

namespace Task1_T.Services.Users
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(string email, string password);
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
