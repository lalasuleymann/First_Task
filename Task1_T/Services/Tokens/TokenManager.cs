using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task1_T.Models.Dtos.Tokens;
using Task1_T.Models.Entities;

namespace Task1_T.Services.Tokens
{
    public class TokenManager : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public async Task<TokenResult> GenerateAuthenticationResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(CreateSecurityKey(_jwtSettings.Secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResult
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

    }
}
