using System.ComponentModel.DataAnnotations;
using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.Users
{
    public class UserDto :BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public int PasswordHash { get; set; }
        public int PasswordSalt  { get; set; }
    }
}
