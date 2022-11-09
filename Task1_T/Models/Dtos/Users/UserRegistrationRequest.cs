using System.ComponentModel.DataAnnotations;

namespace Task1_T.Models.Dtos.Users
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
