using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private const string Key = "user_cache"; // User id istifade ele keyde
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMemoryCache memoryCache;
        public UserManager(IBaseRepository<User> userRepository, ITokenService tokenService, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            this.memoryCache = memoryCache;
        }

        public static string CreatePasswordHash(string password)
        {
            User user = new();
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(password + user.Id));
            return Convert.ToBase64String(hashedDataBytes);
        }

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            AuthResponse response = new();

            var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());

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

        public void AddCache(User[] users)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),
                Size = 1024
            };
            memoryCache.Set<User[]>(Key, users, option);
        }

        public User[] /*List<User>*/ GetCachedUser()
        {
            User[] users;
            if (memoryCache.TryGetValue(Key, out users))
            {
                users = new User[]
            {
                new User{Email = "a@gmail.com", Password= "1234"},
                new User{Email = "l@gmail.com", Password= "4321"}
            };
                AddCache(users);
            }
            return users;
            //List<User> users = memoryCache.Get<List<User>>(Key);
            //return users;
        }
    }
}