using System.ComponentModel.DataAnnotations;

namespace Task1_T.Models.Dtos.Users
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public int PasswordHash { get; set; }
        public int PasswordSalt  { get; set; }
    }
}
