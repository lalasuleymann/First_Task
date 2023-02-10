using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class User : CommonEntity, IActiveEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public bool IsActive { get; set; }
    }
}
