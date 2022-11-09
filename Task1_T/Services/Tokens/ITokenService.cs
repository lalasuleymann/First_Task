using Task1_T.Models.Dtos.Tokens;
using Task1_T.Models.Entities;

namespace Task1_T.Services.Tokens
{
    public interface ITokenService
    {
        Task<TokenResult> GenerateAuthenticationResultForUser(User user);
    }
}
