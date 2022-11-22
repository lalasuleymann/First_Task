using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Task1_T.Middlewares
{
    public class JwtMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly IConfiguration _configuration;

            public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
            {
                _next = next;
                _configuration = configuration;
            }

            public async Task Invoke(HttpContext context)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // bearer adlkaslkdasd

                if (token != null)
                    AttachUserToContext(context, token);

                await _next(context);
            }

            private void AttachUserToContext(HttpContext context, string token)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Base64UrlEncoder.DecodeBytes(_configuration["JwtSettings:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var identity = new ClaimsIdentity(jwtToken.Claims);
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }
    }
}
