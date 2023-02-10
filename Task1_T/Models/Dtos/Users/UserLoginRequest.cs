using System.ComponentModel.DataAnnotations;

namespace Task1_T.Models.Dtos.Users
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
