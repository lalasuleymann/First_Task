using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using Task1_T.Data;
using Task1_T.Models.Departments;
using Task1_T.Models.Dtos.Employees;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;
using static Task1_T.Routes.ApiRoutes;

namespace Task1_T.Services.UserPermissions
{
    public class UserPermissionManager : IUserPermissionService
    {
        private readonly IUnitOfWorkService _unitOfWork;
        public UserPermissionManager(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetUserPermissionOutput>> GetAllUserPermissionsAsync(int id)
        {
            var entities = await _unitOfWork.PermissionRepository.GetPermissionsByUserId(id);
            return entities;
        }


       

        public async Task<List<GetUserPermissionOutput>> GetUserPermissionsWithEmailAsync(string email)
        {
            var entities = await _unitOfWork.PermissionRepository.GetPermissionsByUserEmail(email);
            return entities;
        }


        public async Task CreateUserPermissionsAsync(int userId,SaveUserPermissionRequest request)
        {
            var userPermissionResponse = new UserPermissiongetResponse();

            var userPermissions = request.PermissionIds.Select(permissionId => new Models.Entities.UserPermission
            { 
                UserId = userId,
                PermissionId = permissionId
            }).ToList();
                
            await _unitOfWork.UserPermissions.AddMultipleAsync(userPermissions);

            _unitOfWork.Complete();
        }


        public async Task<bool> CheckUserPermissions(CheckUserPermissions request)
        {
          var userPermissions=await _unitOfWork.PermissionRepository.GetPermissionsByUserEmail(request.Email);
          var hasPermission = userPermissions.Any(x => request.Permissions.Any(p => p.Permission == x.Name));
          return hasPermission;
        }


        public async Task DeleteUserOldPermissions(int userId) 
        {
            await _unitOfWork.PermissionRepository.DeleteAsync(userId);
            _unitOfWork.Complete();
        }
    }
}
