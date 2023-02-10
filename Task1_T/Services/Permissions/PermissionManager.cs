using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Task1_T.Models.Permissions;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;
using Task1_T.Models.Permission;

namespace Task1_T.Services.Permissions
{
    public class PermissionManager : IPermissionService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly IMapper _mapper;
        public PermissionManager(IUnitOfWorkService unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermissionGetResponse> GetPermissionByIdAsync(int permissionId)
        {
            var response = new PermissionGetResponse();
            var Permission = await _unitOfWork.Permissions.GetFirstOrDefaultAsync(x=> x.Id==permissionId);
            response.PermissionDto = _mapper.Map<PermissionDto>(Permission);
            return response;
        }

        public async Task<PermissionGetAllResponse> GetPermissionsAsync()
        {
            var response = new PermissionGetAllResponse();
            var entities = await _unitOfWork.Permissions.GetAllAsync();
            response.PermissionDtos = _mapper.Map<List<PermissionDto>>(entities);
            return response;

        }

        public async Task<PermissionGetResponse> CreatePermissionAsync(SavePermissionRequest request)
        {
            PermissionGetResponse response = new();

            Permission Permission = new Permission
            {
                Name = request.Name
            };

            var addedEntity = await _unitOfWork.Permissions.AddAsync(Permission);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }
             _unitOfWork.Complete();

            var dto = _mapper.Map<PermissionDto>(addedEntity);
            response.PermissionDto = dto;
            return response;
        }

        public async Task UpdatePermissionAsync(int permissionId, SavePermissionRequest request)
        {
            var item = await _unitOfWork.Permissions.GetFirstOrDefaultAsync(x => x.Id == permissionId);
            if (item==null)
            {
                throw new Exception("Permission does not exists!");
            }
            item.Name = request.Name;

            await _unitOfWork.Permissions.UpdateAsync(item);
            _unitOfWork.Complete();
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var entity = await _unitOfWork.Permissions.GetFirstOrDefaultAsync(x => x.Id == permissionId);
            if (entity == null)
            {
                throw new Exception("Permission does not exists!");
            }
            await _unitOfWork.Permissions.DeleteAsync(entity);
            _unitOfWork.Complete();
        }
    }
}
