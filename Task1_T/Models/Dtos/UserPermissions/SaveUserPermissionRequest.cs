using Task1_T.Models.Entities;

namespace Task1_T.Models.Dtos.UserPermissions
{
    public class SaveUserPermissionRequest
    {
        public List<int> PermissionIds { get; set; }
    }
}
