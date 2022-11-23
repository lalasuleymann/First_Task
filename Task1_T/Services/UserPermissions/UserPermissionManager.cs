using AutoMapper;
using Task1_T.Data;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Entities;
using Task1_T.Repositories;

namespace Task1_T.Services.UserPermissions
{
    public class UserPermissionManager : IUserPermissionService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserPermissionManager(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserPermissiongetResponse> CreateUserPermissionsAsync(SaveUserPermissionRequest request)
        {
            var userPermissionResponse = new UserPermissiongetResponse();

            UserPermission userPermission = new UserPermission
            {
                UserId= request.UserId,
                PermissionId=request.PermissionId
            };
            
            var addedUserPermission= await _dbContext.AddAsync(userPermission);
            await _dbContext.SaveChangesAsync();

            var dto = _mapper.Map<UserPermissionDto>(addedUserPermission);
            userPermissionResponse.UserPermissionDto = dto;
            return userPermissionResponse;

        }
    }
}
