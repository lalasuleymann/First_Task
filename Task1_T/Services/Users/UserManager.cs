using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;
using Task1_T.Data;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
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
        private readonly IMemoryCache memoryCache;
        private readonly IMapper _mapper;


        public UserManager(IUnitOfWorkService unitOfWork,IMapper mapper, ITokenService tokenService, IMemoryCache memoryCache,AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            this.memoryCache = memoryCache;
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

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            AuthResponse response = new();

            var existingUser = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());

            if (existingUser != null)
            {
                throw new Exception("User with this mail has already had registered. Try another mail!");
            }

            var newUser = new User
            {
                Email = email.ToLower(),
                Password = CreatePasswordHash(password)
            };

            var createdUser = await _unitOfWork.Users.AddAsync(newUser);
            if (createdUser == null)
            {
                throw new Exception("Problem occured during creating account!");
            }

            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(newUser);
            _unitOfWork.Complete();
            response.Token = generatedToken.Token;
            return response;
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            AuthResponse authResponse = new();

            var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email.ToLower());

            var userHasValidPassword = user.Password == CreatePasswordHash(password);

            if (!userHasValidPassword)
            {
                throw new Exception("Password is not correct!");
            }

            var generatedToken = await _tokenService.GenerateAuthenticationResultForUser(user);
            _unitOfWork.Complete();

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
            var permissions = await memoryCache.GetOrCreateAsync(userId, async (x) => await GetUserPermissions(userId));
            return permissions;
        }
    }
}