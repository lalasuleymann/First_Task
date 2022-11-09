using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class User : CommonEntity, IActiveEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
