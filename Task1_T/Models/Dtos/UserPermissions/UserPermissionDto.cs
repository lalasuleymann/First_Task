using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.UserPermissions
{
    public class UserPermissionDto : BaseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
    }
}
