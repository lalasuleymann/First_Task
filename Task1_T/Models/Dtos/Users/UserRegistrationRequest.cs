using System.ComponentModel.DataAnnotations;

namespace Task1_T.Models.Dtos.Users
{
    public class UserRegistrationRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
