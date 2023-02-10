using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;
using Task1_T.Data;
using Task1_T.Models.Departments;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Models.Permissions;
using Task1_T.Repositories;
using Task1_T.Services.Tokens;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Users
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;


        public UserManager(IUnitOfWorkService unitOfWork, IMapper mapper, ITokenService tokenService, IMemoryCache memoryCache, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _memoryCache = memoryCache;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public static string CreatePasswordHash(string password)
        {
            User user = new();
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(password + user.Id));
            return Convert.ToBase64String(hashedDataBytes);
        }

        public async Task<AuthResponse> RegisterAsync(string name, string surname, string email, string password, string repassword)
        {
            AuthResponse response = new();

            var existingUser = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());

            if (existingUser != null)
            {
                    throw new Exception("User with this mail has already had registered. Try another mail!");
            }

            var newUser = new User
            {
                Name = name,
                Surname = surname,
                Email = email.ToLower(),
                Password = CreatePasswordHash(password),
                RePassword = CreatePasswordHash(repassword)
            };

            if (password != repassword)
            {
                throw new Exception("Enter same Password in both Field");
            }
            var createdUser = await _unitOfWork.Users.AddAsync(newUser);
            if (createdUser == null)
            {
                throw new Exception("Problem occured during creating account!");
            }

            _unitOfWork.Complete();
            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(newUser);
            response.Token = generatedToken.Token;
            return response;

        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            AuthResponse authResponse = new();

            var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }
            var userHasValidPassword = user.Password == CreatePasswordHash(password);

            if (!userHasValidPassword)
            {
                throw new Exception("Password is not correct!");
            }

            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(user);

            authResponse.Token = generatedToken.Token;
            return authResponse;
        }

        public async Task<ICollection<PermissionDto>> GetUserPermissions(int userId)
        {
            var permissions = await _dbContext.UserPermissions.Where(x => x.UserId == userId).Include(x => x.Permission).ToListAsync();
            var userPermissions = _mapper.Map<ICollection<PermissionDto>>(permissions);
            return userPermissions;
        }

        public async Task<ICollection<PermissionDto>> CacheUserPermissions(int userId)
        {
            var permissions = await _memoryCache.GetOrCreateAsync(userId, async (x) => await GetUserPermissions(userId));
            _memoryCache.Set(userId, permissions, TimeSpan.FromMinutes(10));
            return permissions;
        }

        public async Task<GetAllUserResponses> GetSignedUsers()
        {
            var response = new GetAllUserResponses();
            var entities = await _unitOfWork.Users.GetAllAsync();
            response.UserDtos = _mapper.Map<List<UserDto>>(entities);
            return response;
        }

        //public async Task<string> GetUserEmail()
        //{
        //    var email = _unitOfWork.Users.GetFirstOrDefaultAsync(x=>x.Email);
        //    return email;
        //}
    }
}