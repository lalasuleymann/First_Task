using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.Services.Tokens;

namespace Task1_T.Services.Users
{
    public class UserManager : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenService _tokenService;

        public UserManager(IBaseRepository<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public static string CreatePasswordHash(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(password));
            return Convert.ToBase64String(hashedDataBytes);
        }

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            AuthResponse response = new();

            var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == email);

            if (existingUser == null)
            {
                throw new Exception("User with this mail has already had registered. Try another mail!");
            }

            var newUser = new User
            {
                Email = email.ToLower(),
                Password = CreatePasswordHash(password)
            };

            var createdUser = await _userRepository.AddAsync(newUser);
            if (createdUser == null)
            {
                throw new Exception("Problem occured during creating account!");
            }

            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(newUser);

            response.Token = generatedToken.Token;
            return response;
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            AuthResponse authResponse = new();

            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());

            var userHasValidPassword = user.Password == CreatePasswordHash(password);

            if (!userHasValidPassword)
            {
                throw new Exception("Password is not correct!");
            }

            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(user);

            authResponse.Token = generatedToken.Token;

            return authResponse;
        }

    }
}
