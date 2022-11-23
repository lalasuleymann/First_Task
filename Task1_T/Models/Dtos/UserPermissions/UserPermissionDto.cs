using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.UserPermissions
{
    public class UserPermissionDto:BaseDto
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
