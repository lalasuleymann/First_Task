using Task1_T.Models.Shared;

namespace Task1_T.Models.Entities
{
    public class UserPermission : BaseEntity
    {
        public int UserId { get; set; }
        public int PermissionnId { get; set; }

        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}
