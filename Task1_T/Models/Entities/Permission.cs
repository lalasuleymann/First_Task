using System.Collections;
using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class Permission : CommonEntity
    {
        public string? Name { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
