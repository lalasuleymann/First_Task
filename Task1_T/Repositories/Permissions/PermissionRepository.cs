using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using Task1_T.Data;
using Task1_T.Models.Dtos.UserPermissions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Repositories.Base;

namespace Task1_T.Repositories.Permissions
{
    public class PermissionRepository:BaseRepository<Permission>, IPermissionRepository
    {

        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context):base(context)
        {
            _context= context;
        }

        public async Task<List<GetUserPermissionOutput>> GetPermissionsByUserEmail(string email)
        {
            var permissions = await GetPemissionsInternal(email: email);

            return permissions;
        }


        public async Task<List<GetUserPermissionOutput>> GetPermissionsByUserId(int id)
        {
            var permissions = await GetPemissionsInternal(id: id);

            return permissions;
        }


        private  async Task<List<GetUserPermissionOutput>> GetPemissionsInternal(int? id = null, string? email = null)
        {
            var permissionsQuery =
               from up in _context.UserPermissions
               join p in _context.Permissions
          on up.PermissionId equals p.Id
               join u in _context.Users
               on up.UserId equals u.Id
               select new { Permission = p, UserPermission = up, User = u };

            if (id.HasValue)
            {
                permissionsQuery = permissionsQuery.Where(p => p.User.Id == id);
            }
            else 
            {
                permissionsQuery = permissionsQuery.Where(p => p.User.Email == email);
            }

            var permissions = await (from p in permissionsQuery
            select new GetUserPermissionOutput
            {
                Id = p.Permission.Id,
                Name = p.Permission.Name
            }).ToListAsync();

            return permissions;
        }

        public async Task DeleteAsync(int userId)
        {
            var permissions = await
              (from up in _context.UserPermissions
               join p in _context.Permissions
          on up.PermissionId equals p.Id
               join u in _context.Users
               on up.UserId equals u.Id
               where up.UserId == userId
               select up).ToListAsync();

            foreach (var permission in permissions)
            {
                if (permission != null)
                {
                    _context.UserPermissions.Remove(permission);
                }
            }
        }
    }
}
